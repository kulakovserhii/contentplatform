using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.ExternalApi.TmdbModels;
using ContentPlatform.Helpers;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.Services.Implementations
{
    public class TVShowService(HttpClient httpClient, IConfiguration configuration) : ITvShowService
    {
        private readonly string _apiKey = configuration["Tmdb:ApiKey"];
        public async Task<List<EpisodeCreateDto>> GetEpisodesForSeasonsAsync(int tvShowId, int seasonNumber)
        {
            var url = $"tv/{tvShowId}/season/{seasonNumber}?api_key={_apiKey}&language=uk-UA";
            var response = await httpClient.GetFromJsonAsync<TmdbSeasonResponse>(url);
            return response?.Episodes.Select(e => new EpisodeCreateDto
            {
                Title = e.Name ?? $"Епізод {e.SeasonNumber}",
                Description = string.IsNullOrEmpty(e.Overview) ? "Опис серії скоро з'явиться тут": e.Overview,
                ReleaseDate = DateOnly.Parse(e.AirDate),
                ReleaseYear = DateTime.Parse(e.AirDate).Year,
                EpisodeNumber = e.EpisodeNumber,
                EpisodeSeasonNumber = e.SeasonNumber,
                EpisodeRuntimeInMinutes = e.Runtime ?? 45,
                ImageUrl = string.IsNullOrEmpty(e.StillPath) ? null : $"https: //image.tmdb.org/t/p/w500{e.StillPath}",
                ExternalId = $"tmdb-ep-{e.Id}"
            }).ToList() ?? new List<EpisodeCreateDto>();
        }

        public async Task<List<TVShowCreateDto>> GetPopularTvShowsAsync(int count)
        {
            var url = $"tv/popular?api_key={_apiKey}&language=uk-UA&page=1";
            var response = await httpClient.GetFromJsonAsync<TmdbTvListResponse>(url);
            var results = response?.Results.Take(count) ?? new List<TmdbTvResult>();
            var tvShows = new List<TVShowCreateDto>();
            foreach(var item in results)
            {
                var detailedUrl = $"tv/{item.Id}?api_key={_apiKey}&language=uk-UA&append_to_response=credits";
                var details = await httpClient.GetFromJsonAsync<TmdbTvResponse>(detailedUrl);
                if (details != null)
                    tvShows.Add(MapToTvShowDto(details));
            }
            return tvShows;
        }

        private TVShowCreateDto MapToTvShowDto(TmdbTvResponse response)
        {
            return new TVShowCreateDto
            {
                Title = response.Name,
                Description = response.Overview,
                ReleaseDate = DateOnly.Parse(response.FirstAirDate ?? "2000-01-01"),
                ReleaseYear = DateTime.Parse(response.FirstAirDate ?? "2000-01-01").Year,
                ImageUrl = string.IsNullOrEmpty(response.PosterPath) ? null : $"https://image.tmdb.org/t/p/w500{response.PosterPath}",
                ExternalId = $"tmdb-tv-{response.Id}",
                TVShowCreators = string.Join(", ", response.CreatedBy?.Select(c => c.Name) ?? ["Unknown"]),
                TVShowDirector = response.Credits?.Crew?.FirstOrDefault(c => c.Job == "Director")?.Name ?? "Unknown",
                TVShowMainCast = string.Join(", ", response.Credits?.Cast?.Take(5).Select(c => c.Name) ?? ["Unknown"]),
                TVShowTotalSeasons = response.TotalSeasons,
                TVShowTotalEpisodes = response.TotalEpisodes,
                TVShowNetworks = string.Join(", ", response.Networks?.Select(n => n.Name) ?? ["Unknown"]),
                TVShowEndDate = string.IsNullOrEmpty(response.LastAirDate) ? null : DateTime.Parse(response.LastAirDate),
                TVShowGenres = MapGenres(response.Genres),
            };
        }
        private List<FilmGenre> MapGenres(List<TmdbGenre> tmdbGenres)
        {
            var result = new List<FilmGenre>();
            if (tmdbGenres == null || !tmdbGenres.Any())
            {
             
                return new List<FilmGenre> { FilmGenre.Other };
            }
            foreach (var gente in tmdbGenres)
            {
                if (FilmGenreMap.genreMap.TryGetValue(gente.Id, out var filmGenre))
                {
                    result.Add(filmGenre);
                }
            }
            return result.Any() ? result.Distinct().ToList() : new List<FilmGenre> { FilmGenre.Other };
        }
    }
}
