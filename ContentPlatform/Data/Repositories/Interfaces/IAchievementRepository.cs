using ContentPlatform.Models;

namespace ContentPlatform.Data.Repositories.Interfaces
{
    public interface IAchievementRepository
    {
        Task<List<Achievement>> GetAllAchievements();
        Task<List<int>> GetUnlockedAchievementsIdsAsync(int userId);
        Task AddUserAchievementAsync(UserAchevement userAchevement);
        Task<bool> HasAchievemntAsync(int userId, int achievementId);
        Task<List<Achievement>> GetLockedAchievementsAsync(int userId);
    }
}
