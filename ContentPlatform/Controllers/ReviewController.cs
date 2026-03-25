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
    }
}
