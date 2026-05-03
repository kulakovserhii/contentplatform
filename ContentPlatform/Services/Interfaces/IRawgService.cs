using ContentPlatform.Dto_s;

namespace ContentPlatform.Services.Interfaces
{
    public interface IRawgService
    {
        Task<List<GameCreateDto>> GetPopularGamesAsync(int count);
    }
}
