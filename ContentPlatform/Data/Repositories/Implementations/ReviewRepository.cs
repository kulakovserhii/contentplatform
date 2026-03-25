using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentPlatform.Data.Repositories.Implementations
{
    public class ReviewRepository(AppDbContext appDbContext): IReviewRepository
    {
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
