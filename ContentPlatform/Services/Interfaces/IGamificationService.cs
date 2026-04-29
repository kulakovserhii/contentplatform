using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface IGamificationService
    {
        Task ProcessReviewAchievementsAsync (int userId, Review review, Content content);
        Task ProcessLikeAchievementsAsync(int voterId, int authorId, int reviewId, bool isLike);
        Task UndoLikeAchievementAsync(int voterId, int authorId);
        Task ProcessRegistrationAchievementAsync(int userId);
    }
}
