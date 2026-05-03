using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.ExternalApi.RawgModels;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.Services.Implementations
{
    public class RawgService(HttpClient httpClient, IConfiguration configuration) : IRawgService
    {
        private readonly string _apiKey = configuration["Rawg:ApiKey"];
        private const string BaseUrl = "https://api.rawg.io/api/games";
        public async Task<List<GameCreateDto>> GetPopularGamesAsync(int count)
        {
            var result = new List<GameCreateDto>();
            int page = 1;
            int pageSize = 40;
            while (result.Count < count)
            {
                var url = $"{BaseUrl}?key={_apiKey}&ordering=-relevance&page_size={count}";
                var response = await httpClient.GetFromJsonAsync<RawgResponse>(url);
                if (response?.Results == null)
                    return result;
                foreach (var rawGame in response.Results)
                {
                    var detailUrl = $"{BaseUrl}/{rawGame.Id}?key={_apiKey}";
                    var detail = await httpClient.GetFromJsonAsync<RawgDetailResponse>(detailUrl);
                    if (detail == null)
                        continue;
                    result.Add(new GameCreateDto
                    {
                        Title = detail.Name,
                        Description = detail.DescriprionRaw ?? detail.Description ?? "Unknown",
                        ReleaseDate = DateOnly.FromDateTime(detail.Released ?? DateTime.UtcNow),
                        ReleaseYear = detail.Released?.Year ?? 0,
                        ImageUrl = detail.BackgroundImage,
                        ExternalId = $"rawg-{detail.Id}",
                        GameDeveloper = detail.Developers?.FirstOrDefault()?.Name ?? "Unknown",
                        GamePublisher = detail.Publishers?.FirstOrDefault()?.Name ?? "Unknown",
                        GameGenres = MapGenres(detail.Genres),
                        GamePlatforms = MapPlatforms(detail.Platforms)
                    });
                }
                page++;
            }
            return result;
        }
        private List<GameGenre>MapGenres(List<RawgGenre> rawGenres)
        {
            if (rawGenres == null)
                return [];
            var result = new HashSet<GameGenre>();
            foreach(var g in rawGenres)
            {
                var genre = g.Slug.ToLower() switch
                {
                    "role-playing-games-rpg" or "rpg" => GameGenre.RPG,
                    "action" => GameGenre.Action,
                    "strategy" => GameGenre.Strategy,
                    "shooter" => GameGenre.Shooter,
                    "adventure" => GameGenre.Adventure,
                    "puzzle" => GameGenre.Puzzle,
                    "racing" => GameGenre.Racing,
                    "sports" => GameGenre.Sports,
                    "simulation" => GameGenre.Simulation,
                    "massively-multiplayer" => GameGenre.MMO,
                    "fighting" => GameGenre.Fighting,
                    "platformer" => GameGenre.Platformer,
                    _ => (GameGenre?)null
                };
                if (genre.HasValue)
                    result.Add(genre.Value);
            }
            return result.ToList();
        }
        private List<Platform> MapPlatforms(List<RawgPlatformWrapper> rawPlatforms)
        {
            if (rawPlatforms == null)
                return [];
            var result = new HashSet<Platform>();
            foreach(var p in rawPlatforms)
            {
                var platform = p.Platform.Slug.ToLower() switch
                {
                    "pc" => Platform.PC,
                    "playstation5" or "playstation-5" => Platform.PS5,
                    "playstation4" or "playstation-4" => Platform.PS4,
                    "xbox-series-x" or "xbox-one" or "xbox360" => Platform.XBox,
                    "nintendo-switch" => Platform.NintendoSwitch,
                    "ios" or "android" => Platform.Mobile,
                    _ => (Platform?)null
                };
                if (platform.HasValue)
                    result.Add(platform.Value);  
            }
            if (result.Count > 3)
                result.Add(Platform.CrossPlatform);
            return result.ToList();
        }
    }
}
