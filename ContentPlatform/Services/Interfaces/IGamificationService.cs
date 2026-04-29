using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface IGamificationService
    {
        Task ProcessReviewAchievements(int userId, Content content);
        Task ProcessLikeAchievementsAsync(int authorId);
    }
}
