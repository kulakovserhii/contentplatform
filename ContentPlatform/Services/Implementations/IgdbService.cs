using ContentPlatform.Dto_s;
using ContentPlatform.ExternalApi.IgdbModels;
using ContentPlatform.Helpers;
using ContentPlatform.Services.Interfaces;
using System.Data;
using ContentPlatform.Enums;

namespace ContentPlatform.Services.Implementations
{
    public class IgdbService : IIgdbService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly HttpClient _httpClient;
        public IgdbService(HttpClient httpclient, IConfiguration configuration)
        {
            _clientId = configuration["IGDB:ClientId"];
            _clientSecret = configuration["IGDB:ClientSecret"];
            _httpClient = httpclient;
        }
        public async Task<List<GameCreateDto>> GetPopularGamesAsync(int count)
        {
            var token = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Client-ID", _clientId);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var query = $@"fields name, summary, first_release_date, cover.url, 
                platforms.name, genres.name, involved_companies.developer,
                involved_companies.publisher, involved_companies.company.name;
                sort rating_count desc; 
                where rating_count != n & summary != n;
                limit {count};";
            var response = await _httpClient.PostAsync("https://api.igdb.com/v4/games", new StringContent(query));
            var rawGames = await response.Content.ReadFromJsonAsync<List<IgdbGameResponse>>();
            return rawGames?.Select(MapToDto).ToList() ?? new List<GameCreateDto>();

        }
        private async Task<string> GetAccessTokenAsync()
        {
            var authUrl = $"https://id.twitch.tv/oauth2/token?client_id={_clientId}&client_secret={_clientSecret}&grant_type=client_credentials";
            var response = await _httpClient.PostAsync(authUrl, null);
            var authData = await response.Content.ReadFromJsonAsync<IgdbAuthResponse>();
            return authData?.AccessToken ?? throw new Exception("Failed to load token from IGDB");
        }
        private GameCreateDto MapToDto(IgdbGameResponse response)
        {
            var developer = response.InvolvedCompanies?.FirstOrDefault(c => c.IsDeveloper)?.Company?.Name ?? "Unknown Developer";
            var publisher = response.InvolvedCompanies?.FirstOrDefault(c => c.IsPublisher)?.Company?.Name ?? "Unknown Publisher";
            DateTime? releaseDate = response.FirstReleaseDate.HasValue ? DateTimeOffset.FromUnixTimeSeconds(response.FirstReleaseDate.Value).DateTime : null;
            return new GameCreateDto
            {
                Title = response.Name,
                Description = response.Summary,
                GameDeveloper = developer,
                GamePublisher = publisher,
                ReleaseYear = releaseDate?.Year ?? DateTime.Now.Year,
                ReleaseDate = releaseDate.HasValue ? DateOnly.FromDateTime(releaseDate.Value) : default,
                ImageUrl = response.Cover?.Url?.Replace("t_thumb", "t_cover_big").Insert(0, "https"),
                ExternalId = $"igdb-{response.Name.Replace(" ", "-").ToLower()}",
                GameGenres = response.Genres?.Select(x => IgdbMap.GenreMap.GetValueOrDefault(x.Name, GameGenre.Sandbox)).Distinct().ToList() ?? new(),
                GamePlatforms = response.Platforms?.Select(x => IgdbMap.PlatformMap.GetValueOrDefault(x.Name, Platform.PC)).Distinct().ToList() ?? new(),
            };
        }
    }
}
