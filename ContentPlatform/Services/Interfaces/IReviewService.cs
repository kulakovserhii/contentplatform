using ContentPlatform.Dto_s;
using ContentPlatform.Enums;

namespace ContentPlatform.Services.Interfaces
{
    public interface IReviewService
    {
        Task<string> LeaveReviewAsync(CreateReviewDto createReviewDto, int contentId, int userId);
        Task<string> EvaluateReviewAsync(int reviewId, int userId, Evaluate evaluate);
    }
}
