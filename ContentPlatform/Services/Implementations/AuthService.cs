using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Dto_s;
using ContentPlatform.Models;
using ContentPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ContentPlatform.Services.Implementations
{
    public class AuthService(IAuthRepository authRepository, IGamificationService gamificationService, IConfiguration configuration) : IAuthService
    {
        public async Task<TokensDto?> LoginAsync(LoginDto loginDto)
        {
            var userExists = await authRepository.GetUserByEmailAsync(loginDto.Email);
            if (userExists == null)
                return null;
            var passwordHasher = new PasswordHasher<object>();
            var passwordCorrect = passwordHasher.VerifyHashedPassword(null!, userExists.Password, loginDto.Password);
            if (passwordCorrect == PasswordVerificationResult.Failed)
                return null;
            return await ReturnTokens(userExists);
        }

        public async Task<bool> LogoutAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            var refreshToken = await authRepository.GetUserRefreshTokenAsync(token);
            if (refreshToken == null)
                return false;
            await authRepository.RemoveRefreshTokenAsync(refreshToken);
            return true;
        }

        public async Task<string?> RegisterAsync(RegisterDto registerDto)
        {
            var userExists = await authRepository.GetUserByEmailAsync(registerDto.Email);
            if (userExists != null)
                return "User already has an account";
            var validationError = RegisterValidation(registerDto.Email, registerDto.Password);
            if (validationError != null)
                return validationError;
            var passwordHasher = new PasswordHasher<object>();
            var hashPassword = passwordHasher.HashPassword(null!, registerDto.Password);
            var user = new User
            {
                Email = registerDto.Email,
                Password = hashPassword,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Age = registerDto.Age
            };
            await authRepository.CreateUserAsync(user);
            await gamificationService.ProcessRegistrationAchievementAsync(user.Id);
            return "Account created";
        }
        public async Task<User?> GetOrCreateUserAsync(string email, string? name, string? lastname)
        {
            var userExists = await authRepository.GetUserByEmailAsync(email);
            if (userExists != null)
                return userExists;
            var passwordHasher = new PasswordHasher<object>();
            var hashPassword = passwordHasher.HashPassword(null!, Guid.NewGuid().ToString());
            var user = new User
            {
                Email = email,
                Password = hashPassword,
                FirstName = name ?? email.Split('@')[0],
                LastName = lastname ?? email.Split('@')[0],
                Age = null
            };
            await authRepository.CreateUserAsync(user);
            return user;
        }
        private async Task<string> CreateJwtToken(User user)
        {
            var userRoles = await authRepository.GetUserRolesAsync(user.Id);
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };
            foreach(var role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AuthSettings:Token").Value!));
            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetSection("AuthSettings:Issuer").Value,
                audience: configuration.GetSection("AuthSettings:Audience").Value,
                signingCredentials: creds,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(90)
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        private async Task<string> SaveRefreshToken(User user)
        {
            const int MAX_TOKENS = 3;
            var userTokens = await authRepository.GetRefreshTokensAsync(user.Id);
            var expiredTokens = userTokens.Where(ut => ut.ExpiresAt <= DateTime.UtcNow).ToList();
            foreach (var expiredToken in expiredTokens)
            {
                await authRepository.RemoveRefreshTokenAsync(expiredToken);
            }
            userTokens = await authRepository.GetRefreshTokensAsync(user.Id);
            if (userTokens.Count >= MAX_TOKENS)
            {
                var sortingTokens = userTokens.OrderBy(ut => ut.ExpiresAt).Take(userTokens.Count - MAX_TOKENS + 1);
                foreach (var token in sortingTokens)
                {
                    await authRepository.RemoveRefreshTokenAsync(token);
                }
            }
            var newToken = new RefreshToken
            {
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                Token = GenerateRefreshToken(),
            };
            await authRepository.CreateRefreshTokenAsync(newToken);
            return newToken.Token;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private async Task<TokensDto> ReturnTokens(User? userExists)
        {
            return new TokensDto
            {
                JwtToken = await CreateJwtToken(userExists),
                RefreshToken = await SaveRefreshToken(userExists),
            };
        }
        private string RegisterValidation(string email, string password)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d).{8,}$";
            bool isEmailValid = Regex.IsMatch(email, emailPattern);
            bool isPasswordValid = Regex.IsMatch(password, passwordPattern);
            if (!Regex.IsMatch(email, emailPattern))
                return "Invalid email format";
            if (!Regex.IsMatch(password, passwordPattern))
                return "Password must be at least 8 characters and contain letters and numbers";
            return null;
        }


        public async Task<TokensDto> GetTokensAsync(User user)
        {
            return new TokensDto
            {
                JwtToken = await CreateJwtToken(user),
                RefreshToken = await SaveRefreshToken(user),
            };
        }
    }
}
