using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Models;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.Services.Implementations
{
    public class GamificationService(IUserStatsRepository userStatsRepository) : IGamificationService
    {
        public async Task ProcessLikeAchievementsAsync(int authorId)
        {
            var userStats = await userStatsRepository.GetUserStatsAsync(authorId);
            if (userStats == null)
            {
                userStatsRepository.CreateAsync(new UserStats());
            }
        }

        public Task ProcessReviewAchievements(int userId, Content content)
        {
            throw new NotImplementedException();
        }
    }
}
