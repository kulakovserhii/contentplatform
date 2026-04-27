using ContentPlatform.Dto_s;

namespace ContentPlatform.Services.Interfaces
{
    public interface ITmdbService
    {
        Task<List<FilmCreateDto>> GetPopularFilmsAsync(int count);
    }
}
