using ContentPlatform.Dto_s;
using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface ITvShowService
    {
        Task<List<TVShowCreateDto>> GetPopularTvShowsAsync(int count);
        Task<List<EpisodeCreateDto>> GetEpisodesForSeasonsAsync(int tvShowId);
    }
}
