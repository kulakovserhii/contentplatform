using ContentPlatform.Data.Repositories.Implementations;
using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Models;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.Services.Implementations
{
    public class GamificationService(IUserStatsRepository userStatsRepository, IAchievementRepository achievementRepository, IReviewRepository reviewRepository) : IGamificationService
    {
        public async Task ProcessLikeAchievementsAsync(int voterId, int authorId, int reviewId, bool isLike)
        {
            if (!isLike)
                return;
            var authorStats = await userStatsRepository.GetUserStatsAsync(authorId);
            authorStats.LikeReviewsRetrievedCount++;
            await userStatsRepository.UpdateAsync(authorStats);
            var voterStats = await GetOrInitializedStats(voterId);
            voterStats.LikeReviewsCount++;
            await userStatsRepository.UpdateAsync(voterStats);
            bool alreadyHas = await achievementRepository.HasAchievemntAsync(authorId, 12);
            if (!alreadyHas)
            {
                var likeCounter = await reviewRepository.GetLikesCount(reviewId);
                if(likeCounter >= 30)
                {
                    await achievementRepository.AddUserAchievementAsync(new UserAchevement
                    {
                        UserId = authorId,
                        AchievementId = 12,
                    });
                }
            }
            int totalreviews = authorStats.MusicReviewsCount + authorStats.FilmsReviewsCount
               + authorStats.BookReviewsCount + authorStats.GameReviewsCount + authorStats.TvShowReviewsCount;
            await CheckAndAwardAsync(authorId, authorStats, totalreviews);
        }

        public async Task ProcessReviewAchievementsAsync(int userId, Review review,Content content)
        {
            var userStats = await GetOrInitializedStats(userId);
            if (content is Music)
                userStats.MusicReviewsCount++;
            else if (content is Film)
                userStats.FilmsReviewsCount++;
            else if (content is Book)
                userStats.BookReviewsCount++;
            else if (content is Game)
                userStats.GameReviewsCount++;
            else if(content is TVShow || content is Episode)
                userStats.TvShowReviewsCount++;
            if (review.Rating == 10)
                userStats.PerfectiRatingCount++;
            if (review.Rating <= 3)
                userStats.LowRatingsCount++;
            await userStatsRepository.UpdateAsync(userStats);
            int totalreviews = userStats.MusicReviewsCount + userStats.FilmsReviewsCount
                + userStats.BookReviewsCount + userStats.GameReviewsCount + userStats.TvShowReviewsCount;
            await CheckAndAwardAsync(userId, userStats, totalreviews);
        }

        private async Task<UserStats> GetOrInitializedStats(int userId)
        {
            var userStats = await userStatsRepository.GetUserStatsAsync(userId);
            if(userStats == null)
            {
                userStats = new UserStats
                {
                    UserId = userId
                };
                await userStatsRepository.CreateAsync(userStats);
            }
            return userStats;
        }
        private async Task CheckAndAwardAsync(int userId, UserStats userStats, int totalreviews)
        {
            var lockedAchievements = await achievementRepository.GetLockedAchievementsAsync(userId);
            var awardedIds = new HashSet<int>();
            foreach(var achievement in lockedAchievements)
            {
                if (awardedIds.Contains(achievement.Id))
                    continue;
                bool isEligible = achievement.Id switch
                {
                    1 => totalreviews >= 1,
                    2 => userStats.LikeReviewsRetrievedCount >= 1,
                    3 => totalreviews >= 15,
                    4 => userStats.LikeReviewsRetrievedCount >= 100,
                    5 => true,
                    6 => userStats.FilmsReviewsCount >= 5,
                    7 => userStats.TvShowReviewsCount >= 5,
                    8 => userStats.MusicReviewsCount >= 5,
                    9 => userStats.GameReviewsCount >= 5,
                    10 => userStats.BookReviewsCount >= 5,
                    11 => userStats.LikeReviewsCount >= 50,
                    13 => userStats.LikeReviewsRetrievedCount >= 500,
                    14 => userStats.PerfectiRatingCount >= 5,
                    15 => userStats.LowRatingsCount >= 5,
                    _ => false,
                };
                if (isEligible)
                {
                    bool exists = await achievementRepository.HasAchievemntAsync(userId, achievement.Id);
                    if (!exists)
                    {
                        await achievementRepository.AddUserAchievementAsync(new UserAchevement
                        {
                            UserId = userId,
                            AchievementId = achievement.Id,
                            UnlockedAt = DateTime.UtcNow,
                        });
                    }       
                }
            }
        }

        public async Task UndoLikeAchievementAsync(int voterId, int authorId)
        {
            var voterStats = await GetOrInitializedStats(voterId);
            if(voterStats != null && voterStats.LikeReviewsCount > 0)
            {
                voterStats.LikeReviewsCount--;
                await userStatsRepository.UpdateAsync(voterStats);
            }
            var authorStats = await GetOrInitializedStats(authorId);
            if(authorStats != null && authorStats.LikeReviewsRetrievedCount > 0)
            {
                authorStats.LikeReviewsRetrievedCount--;
                await userStatsRepository.UpdateAsync(authorStats);
            }
        }

        public async Task ProcessRegistrationAchievementAsync(int userId)
        {
            var stats = await GetOrInitializedStats(userId);
            await CheckAndAwardAsync(userId, stats, 0);
        }
    }
}
