using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentPlatform.Data.Repositories.Implementations
{
    public class AchievementRepository(AppDbContext appDbContext) : IAchievementRepository
    {
        public async Task AddUserAchievementAsync(UserAchevement userAchevement)
        {
            await appDbContext.UserAchevements.AddAsync(userAchevement);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<List<Achievement>> GetAllAchievements()
        {
            var achievemts = await appDbContext.Achievements.ToListAsync();
            return achievemts;
        }

        public async Task<List<Achievement>> GetLockedAchievementsAsync(int userId)
        {
            var unlockedAchievementsIds = await appDbContext.UserAchevements
                .Where(ua => ua.Id == userId).Select(ua => ua.Id).ToListAsync();
            var unlockedAchievements = await appDbContext.Achievements
                .Where(a => !unlockedAchievementsIds.Contains(a.Id)).ToListAsync();
            return unlockedAchievements;
        }

        public async Task<List<int>> GetUnlockedAchievementsIdsAsync(int userId)
        {
            var userAcievements = await appDbContext.UserAchevements
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.AchievementId).ToListAsync();
            return userAcievements;
        }

        public async Task<bool> HasAchievemntAsync(int userId, int achievementId)
        {
            var achievementExists = await appDbContext.UserAchevements
                .AnyAsync(ua => ua.AchievementId == achievementId && ua.UserId == userId);
            return achievementExists;
        }
    }
}
