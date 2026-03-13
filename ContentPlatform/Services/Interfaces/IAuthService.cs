using ContentPlatform.Dto_s;
using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokensDto?> LoginAsync(LoginDto loginDto);
        Task<string?> RegisterAsync(RegisterDto registerDto);
        Task<bool> LogoutAsync(string refreshToken);
    }
}
