using ContentPlatform.Models;

namespace ContentPlatform.Data.Repositories.Interfaces
{
    public interface IUserStatsRepository
    {
        Task<UserStats> GetUserStatsAsync(int userId);
        Task CreateAsync(UserStats userStats);
        Task UpdateAsync(UserStats userStats);
    }
}
