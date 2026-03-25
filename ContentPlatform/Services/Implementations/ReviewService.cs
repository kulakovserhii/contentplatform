using AutoMapper;
using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Models;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.Services.Implementations
{
    public class ReviewService(IReviewRepository reviewRepository, 
        IContentRepository contentRepository, IMapper mapper) : IReviewService
    {
        public async Task<string> EvaluateReviewAsync(int reviewId, int userId, Evaluate evaluate)
        {
            var review = await reviewRepository.GetReviewById(reviewId);
            if (review == null)
                return "Review was not found";
            var ratereview = await reviewRepository.GetRateReview(userId, reviewId);
            if(ratereview == null)
            {
                var rr = new RateReview
                {
                    UserId = userId,
                    ReviewId = reviewId,
                    VoteType = evaluate
                };
                await reviewRepository.LeaveRateReview(rr);
                return "Review was rated";
            }
            if (evaluate == ratereview.VoteType)
            {
                await reviewRepository.RemoveRateReview(ratereview);
                return "Review was deleted";
            }
            else 
            {
                ratereview.VoteType = evaluate;
                await reviewRepository.UpdateRateReview(ratereview);
                return "Review`s rate was changed";
            }
        }

        public async Task<string> LeaveReviewAsync(CreateReviewDto createReviewDto, int contentId, int userId)
        {
            var contentExists = await contentRepository.GetContentByIdAsync<Content>(contentId);
            if (contentExists == null)
                return "Content wasn`t found";
            if (createReviewDto.Rating < 1 || createReviewDto.Rating > 10)
                return "Review rating must be from 1 to 10";
            var reviewExists = await reviewRepository.GetReviewByUserIdContentInd(userId, contentId);
            if(reviewExists != null)
            { 
                reviewExists.Rating = createReviewDto.Rating;
                reviewExists.Title = createReviewDto.Title;
                reviewExists.Description = createReviewDto.Description;
                await reviewRepository.UpdateReview(reviewExists);
                await contentRepository.UpdateContentRating(contentId);
                return "Review has been updated";
            }
            var review = new Review
            {
                Rating = createReviewDto.Rating,
                Title = createReviewDto.Title,
                Description = createReviewDto.Description,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                ContentId = contentExists.Id,
            };
            await reviewRepository.LeaveReview(review);
            await contentRepository.UpdateContentRating(contentId);
            return "Review has been created";
        }
    }
}
