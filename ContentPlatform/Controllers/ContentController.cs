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
        [HttpPost("create-film")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFilm([FromForm] FilmCreateDto dto)
        {
            var creation = await contentService.CreateFilmAsync(dto);
            if (creation == null)
                return BadRequest(new { message = "Film creation failed" });
            return Ok(creation);
        }
        [HttpPost("create-tvshow")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTVShow([FromForm] TVShowCreateDto dto)
        {
            var creation = await contentService.CreateTVShowAsync(dto);
            if (creation == null)
                return BadRequest(new { message = "TVShow creation failed" });
            return Ok(creation);
        }
        [HttpPost("create-music")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMusic([FromForm] MusicCreateDto dto)
        {
            var creation = await contentService.CreateMusicAsync(dto);
            if (creation == null)
                return BadRequest(new { message = "Music creation failed" });
            return Ok(creation);
        }
        [HttpPost("create-game")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateContent([FromForm] GameCreateDto dto)
        {
            var creation = await contentService.CreateGameDto(dto);
            if (creation == null)
                return BadRequest(new { message = "Game creation failed" });
            return Ok(creation);
        }
        [HttpPost("create-episode")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateContent([FromForm] EpisodeCreateDto dto)
        {
            var creation = await contentService.CreateEpisodeAsync(dto);
            if (creation == null)
                return BadRequest(new { message = "Episode creation failed" });
            return Ok(creation);
        }
        [HttpPost("create-book")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateContent([FromForm] BookCreateDto dto)
        {
            var creation = await contentService.CreateBookAsync(dto);
            if (creation == null)
                return BadRequest(new { message = "Book creation failed" });
            return Ok(creation);
        }
        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetContent(int contentId)
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
        [HttpPut("film/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFilm(int id, [FromForm] UpdateFilmDto dto)
        {
            var result = await contentService.UpdateFilmAsync(id, dto);
            if (result == null)
                return BadRequest(new { message = "Film update failed" });
            return Ok(result);
        }
        [HttpPut("tvshow/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTvShow(int id, [FromForm] UpdateTVShowDto dto)
        {
            var result = await contentService.UpdateTVShowAsync(id, dto);
            if (result == null)
                return BadRequest(new { message = "TvShow update failed" });
            return Ok(result);
        }
        [HttpPut("music/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMusic(int id, [FromForm] UpdateMusicDto dto)
        {
            var result = await contentService.UpdateMusicAsync(id, dto);
            if (result == null)
                return BadRequest(new { message = "Music update failed" });
            return Ok(result);
        }
        [HttpPut("game/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGame(int id, [FromForm] UpdateGameDto dto)
        {
            var result = await contentService.UpdateGameAsync(id, dto);
            if (result == null)
                return BadRequest(new { message = "Game update failed" });
            return Ok(result);
        }
        [HttpPut("episode/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEpisode(int id, [FromForm] UpdateEpisodeDto dto)
        {
            var result = await contentService.UpdateEpisodeAsync(id, dto);
            if (result == null)
                return BadRequest(new { message = "Episode update failed" });
            return Ok(result);
        }
        [HttpPut("book/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] UpdateBookDto dto)
        {
            var result = await contentService.UpdateBookAsync(id, dto);
            if (result == null)
                return BadRequest(new { message = "Book update failed" });
            return Ok(result);
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
