using ContentPlatform.Dto_s;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.Services.Implementations
{
    public class TVShowService(HttpClient httpClient, IConfiguration configuration) : ITvShowService
    {
        private readonly string _apiKey = configuration["Tmdb:ApiKey"];
        public Task<List<EpisodeCreateDto>> GetEpisodesForSeasonsAsync(int tvShowId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TVShowCreateDto>> GetPopularTvShowsAsync(int count)
        {
            throw new NotImplementedException();
        }
    }
}
