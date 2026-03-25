using ContentPlatform.Models;

namespace ContentPlatform.Data.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> LeaveReview(Review review);
        Task<Review> GetReviewById(int reviewId);
        Task<Review> GetReviewByUserIdContentInd(int userId, int contentId);
        Task<Review> UpdateReview(Review review);
        Task<RateReview> GetRateReview(int userId, int reviewId);
        Task<RateReview> LeaveRateReview(RateReview rateReview);
        Task RemoveRateReview(RateReview rateReview);
        Task UpdateRateReview(RateReview rateReview);
    }
}
