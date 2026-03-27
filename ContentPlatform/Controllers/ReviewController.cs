using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContentPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        [HttpPost("leave-review")]
        [Authorize]
        public async Task<IActionResult> LeaveReviewAsync([FromBody] CreateReviewDto createReviewDto, int contentId)
        {
            var userId = (User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User is unauthorize"});
            var result = await reviewService.LeaveReviewAsync(createReviewDto, contentId, int.Parse(userId));
            if (result == "Content wasn`t found")
                return NotFound(new { message = result });
            else if (result == "Review rating must be from 1 to 10")
                return BadRequest(new { message = result });
            else if (result == "Review has been updated")
                return Ok(new { message = result });
            return Ok(new { message = result });
        }
        [HttpPost("rate-review")]
        [Authorize]
        public async Task<IActionResult> RateReviewAsync(int reviewId, Evaluate evaluate)
        {
            var userId = (User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User is unauthorize" });
            var result = await reviewService.EvaluateReviewAsync(reviewId, int.Parse(userId), evaluate);
            if (result == "Review was not found")
                return NotFound(new { message = result });
            return Ok(new { message = result });
        }
        [HttpGet("my-reviews")]
        [Authorize]
        public async Task<IActionResult> GetMyReviews()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await reviewService.GetUserReviewsAsync(int.Parse(userId!));
            if (result.Count == 0)
                return Ok(new { message = "User has no reviews" });
            return Ok(result);
        }
        [HttpGet("content-reviews")]
        public async Task<IActionResult> GetContentReviews(int contentId, [FromQuery] GetReviewsDto getReviewsDto)
        {
            var result = await reviewService.GetContentReviews(contentId, getReviewsDto);
            if (result == null)
                return NotFound(new { message = "Content has no reviews" });
            return Ok(result);
        }
    }
}
