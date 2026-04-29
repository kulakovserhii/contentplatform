using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Helpers;
using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentPlatform.Data.Repositories.Implementations
{
    public class ReviewRepository(AppDbContext appDbContext): IReviewRepository
    {
        public async Task<List<Review>> GetContentReviews(int contentId, GetReviewsDto getReviewsDto)
        {
            var reviews = await appDbContext.Reviews.Include(r => r.User).Include(r => r.RateReviews)
                .Where(r => r.ContentId == contentId).ToListAsync();
            if (reviews.Count == 0)
                return new List<Review>();
            var popularityCount = reviews.ToDictionary
                 (r => r.Id,
                 r => r.RateReviews.Count(rr => rr.VoteType == Evaluate.Like) 
                 - r.RateReviews.Count(rr => rr.VoteType == Evaluate.Dislike)
                 );
            return getReviewsDto.SortBy switch
            {
                ReviewSortBy.Popular => getReviewsDto.SortType == SortType.Ascending ?
                    reviews.OrderBy(r => popularityCount[r.Id]).ToList() :
                    reviews.OrderByDescending(r => popularityCount[r.Id]).ToList(),
                ReviewSortBy.CreatedAt => getReviewsDto.SortType == SortType.Ascending ?
                    reviews.OrderBy(r => r.CreatedAt).ToList() :
                    reviews.OrderByDescending(r => r.CreatedAt).ToList(),
                _ => reviews.OrderBy(r => popularityCount[r.Id]).ToList(),
            };
        }

        public async Task<int> GetLikesCount(int reviewId)
        {
            var likescount = await appDbContext.RateReviews
                .Where(rr => rr.ReviewId == reviewId && rr.VoteType == Evaluate.Like)
                .CountAsync();
            return likescount;
        }

        public async Task<RateReview> GetRateReview(int userId, int reviewId)
        {
            var rateReview = await appDbContext.RateReviews.Where(rr => rr.UserId == userId && rr.ReviewId == reviewId)
                .FirstOrDefaultAsync();
            return rateReview;
        }

        public async Task<Review> GetReviewById(int reviewId)
        {
            return await appDbContext.Reviews.FindAsync(reviewId);
        }

        public async Task<Review> GetReviewByUserIdContentInd(int userId, int contentId)
        {
            var review = await appDbContext.Reviews.Where(r => r.UserId == userId && r.ContentId == contentId)
                .FirstOrDefaultAsync();
            return review;
        }

        public async Task<List<Review>> GetUserReviews(int userId)
        {
            var userreviews = await appDbContext.Reviews.Include(r => r.Content).Where(r => r.UserId == userId).ToListAsync();
            return userreviews;
        }

        public async Task<Dictionary<int, Evaluate>> GetUserVotesForReviews(int userId, List<int> reviewIds)
        {
            if (reviewIds == null || !reviewIds.Any())
                return new Dictionary<int, Evaluate>();
            var result = await appDbContext.RateReviews.Where(rr => rr.UserId == userId && reviewIds.Contains(rr.ReviewId))
                .ToDictionaryAsync(rr => rr.ReviewId, rr => rr.VoteType);
            return result;
        }

        public async Task<RateReview> LeaveRateReview(RateReview rateReview)
        {
            appDbContext.RateReviews.Add(rateReview);
            await appDbContext.SaveChangesAsync();
            return rateReview;
        }

        public async Task<Review> LeaveReview(Review review)
        {
            appDbContext.Reviews.Add(review);
            await appDbContext.SaveChangesAsync();
            return review;
        }

        public async Task RemoveRateReview(RateReview rateReview)
        {
            appDbContext.RateReviews.Remove(rateReview);
            await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateRateReview(RateReview rateReview)
        {
            appDbContext.RateReviews.Update(rateReview);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<Review> UpdateReview(Review review)
        {
            appDbContext.Reviews.Update(review);
            await appDbContext.SaveChangesAsync();
            return review;
        }
    }
}
