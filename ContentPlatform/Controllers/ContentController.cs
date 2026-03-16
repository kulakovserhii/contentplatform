using ContentPlatform.Dto_s;
using ContentPlatform.Models;
using ContentPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

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
            var content = await contentService.GetContentByIdAsync(contentId);
            if(content == null)
                return NotFound(new { message = "Content not found" });
            return Ok(content);
        }
    }
}
