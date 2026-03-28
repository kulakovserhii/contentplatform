using ContentPlatform.Dto_s;
using ContentPlatform.Models;
using ContentPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Security.Claims;

namespace ContentPlatform.Controllers
{
    [Route("api/content")]
    [ApiController]
    public class ContentController(IContentService contentService) : ControllerBase
    {
        [HttpPost("create-content")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateContent([FromForm] UniversalContentDto dto)
        {
            var creation = await contentService.CreateContentAsync(dto);
            if (creation == null)
                return BadRequest(new { message = "Content creation failed" });
            return Ok(creation);
        }
        [HttpGet("get-content-by-id")]
        public async Task<IActionResult> GetContent([FromQuery] int contentId)
        {
            var userIdExists = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int? userId = userIdExists != null ? int.Parse(userIdExists) : null;
            var content = await contentService.GetContentByIdAsync(contentId, userId);
            if(content == null)
                return NotFound(new { message = "Content not found" });
            return Ok(content);
        }
        [HttpGet("get-all-content")]
        public async Task<IActionResult> GetAllContent()
        {
            var content = await contentService.GetAllContentWithoutReviewsAsync();
            if (content == null || content.Count == 0)
                return NotFound(new {message = "No content were found"});
            return Ok(content);
        }
        [HttpDelete("delete-content")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContent([FromQuery] int contentId)
        {
            var result = await contentService.DeleteContentAsync(contentId);
            if (result == string.Empty)
                return NotFound(new { message = "Content with this id does not exist" });
            if(result == "Content deletion failed")
                return BadRequest(new { message = result });
            return Ok(new { message = result });
        }
        [HttpPut("update-content")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateContent([FromQuery] int contentId, [FromForm] UpdateContentDto dto)
        {
            try
            {
                var update = await contentService.UpdateContentAsync(contentId, dto);
                if (update == null)
                    return BadRequest(new { message = "Content update failed" });
                return Ok(update);
            }
           
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("search-content")]
        public async Task<IActionResult> SearchContent([FromQuery] ContentSearch contentSearch)
        {
            var result = await contentService.SearchContentAsync(contentSearch);
            if (result == null)
                return NotFound(new { message = "No content was found with this parameters" });
            return Ok(result);
        }
        [HttpGet("get-content-small-info")]
        public async Task<IActionResult> GetContentSmallInfo()
        {
            var result = await contentService.GetContentsSmallInfo();
            if (result == null)
                return NotFound(new { message = "No content was found"});
            return Ok(result);
        }
    }
}
