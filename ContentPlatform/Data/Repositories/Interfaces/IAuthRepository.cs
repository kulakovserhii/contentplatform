using ContentPlatform.Models;

namespace ContentPlatform.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserById(int userId);
        Task<List<string>> GetUserRolesAsync(int userId);
        Task<List<RefreshToken>> GetRefreshTokensAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<bool> RemoveRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetUserRefreshTokenAsync(string token);
    }
}
