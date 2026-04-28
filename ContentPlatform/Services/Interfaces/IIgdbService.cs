using ContentPlatform.Dto_s;

namespace ContentPlatform.Services.Interfaces
{
    public interface IIgdbService
    {
        Task<List<GameCreateDto>> GetPopularGamesAsync(int count);
    }
}
