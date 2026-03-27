using ContentPlatform.Dto_s;
using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokensDto?> LoginAsync(LoginDto loginDto);
        Task<string?> RegisterAsync(RegisterDto registerDto);
        Task<bool> LogoutAsync(string refreshToken);
        Task<User?> GetOrCreateUserAsync(string email, string? name, string? lastname);
        Task<TokensDto> GetTokensAsync(User user);
    }
}
