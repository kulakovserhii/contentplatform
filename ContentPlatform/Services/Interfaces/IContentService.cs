using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface IContentService
    {
        Task<Film> CreateFilmAsync(FilmCreateDto filmCreateDto);
        Task<TVShow> CreateTVShowAsync(TVShowCreateDto tVShowCreateDto);
        Task<Episode> CreateEpisodeAsync(EpisodeCreateDto episodeCreateDto, string? externallId = null, string? externalShowId = null);
        Task<Book> CreateBookAsync(BookCreateDto bookCreateDto);
        Task<Music> CreateMusicAsync(MusicCreateDto musicCreateDto);
        Task<Game> CreateGameAsync(GameCreateDto gameCreateDto);
        Task<ContentDetailsDto> GetContentByIdAsync(int contentId, int? userId);
        Task<List<ContentDetailsDto>> GetAllContentByTypeAsync(ContentType contentType);
        Task<List<ContentDetailsDto>> GetAllContentWithoutReviewsAsync();
        Task<string> DeleteContentAsync(int contentId);
        Task<ContentDetailsDto> UpdateFilmAsync(int filmId, UpdateFilmDto updateFilmDto);
        Task<ContentDetailsDto> UpdateTVShowAsync(int tVShowId, UpdateTVShowDto updateTVShowDto);
        Task<ContentDetailsDto> UpdateEpisodeAsync(int episodeId, UpdateEpisodeDto updateEpisodeDto);
        Task<ContentDetailsDto> UpdateBookAsync(int bookId, UpdateBookDto updateBookDto);
        Task<ContentDetailsDto> UpdateMusicAsync(int musicId, UpdateMusicDto updateMusicDto);
        Task<ContentDetailsDto> UpdateGameAsync(int gameId, UpdateGameDto updateGameDto);
        Task<List<ContentDetailsDto>> SearchContentAsync(ContentSearch contentSearch);
        Task<List<ContentSmallInfo>> GetContentsSmallInfo();
    }
}
