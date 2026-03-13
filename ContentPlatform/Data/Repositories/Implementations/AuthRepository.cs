using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ContentPlatform.Data.Repositories.Implementations
{
    public class AuthRepository(AppDbContext appDbContext) : IAuthRepository
    {
        public async Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            appDbContext.RefreshTokens.Add(refreshToken);
            await appDbContext.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var userRoleEntity = await appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            if (userRoleEntity == null)
                return null;
            if (!user.UserRoles.Any())
            {
                user.UserRoles.Add(new UserRole
                    {
                        RoleId = userRoleEntity.Id,
                        UserId = user.Id,
                        AssignedAt = DateTime.UtcNow
                    }
                );
            }
            appDbContext.Users.Add(user);
            await appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<RefreshToken>> GetRefreshTokensAsync(int userId)
        {
            var refreshTokens = await appDbContext.RefreshTokens
                .Where(rt => rt.UserId == userId).ToListAsync();
            return refreshTokens;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await appDbContext.Users.Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<RefreshToken> GetUserRefreshTokenAsync(string token)
        {
            return await appDbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            var user = await appDbContext.Users.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Id == userId);
            if(user != null)
            {
                var roles = user.UserRoles.Select(u => u.Role.Name).ToList();
                return roles;
            }
            return new List<string>();
        }

        public async Task<bool> RemoveRefreshTokenAsync(RefreshToken refreshToken)
        { 
            appDbContext.RefreshTokens.Remove(refreshToken);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<RefreshToken> UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            appDbContext.RefreshTokens.Update(refreshToken);
            await appDbContext.SaveChangesAsync();
            return refreshToken;
        }
    }
}
