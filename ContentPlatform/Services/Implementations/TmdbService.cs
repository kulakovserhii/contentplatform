using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.ExternalApi.TmdbModels;
using ContentPlatform.Helpers;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.Services.Implementations
{
    public class TmdbService(HttpClient httpClient, IConfiguration configuration, ILogger<TmdbService> logger) : ITmdbService
    {
        private readonly string _apiKey = configuration["TMDB:ApiKey"];

        public async Task<List<FilmCreateDto>> GetPopularFilmsAsync(int count)
        {
            var filmsToCreate = new List<FilmCreateDto>();
            int page = 1;
            while (filmsToCreate.Count < count && page <= 30)
            {
                var response = await httpClient.GetFromJsonAsync<TmdbDiscoverResponse>($"discover/movie?sort_by=popularity.desc&language=uk-UA&page={page}");
                if (response == null || !response.Results.Any())
                    break;

                foreach (var movieSummary in response.Results)
                {
                    if (filmsToCreate.Count >= count)
                        break;
                    var details = await httpClient.GetFromJsonAsync<TmdbMovieDetails>($"movie/{movieSummary.Id}?append_to_response=credits&language=uk-UA");
                    if (details != null && !string.IsNullOrEmpty(details.Title) && !string.IsNullOrEmpty(details.Overview))
                        filmsToCreate.Add(MapToFilmCreateDto(details));
                    else
                    {
                        logger.LogInformation("Skipping movie with ID {MovieId} due to missing details", movieSummary.Id);
                    }
                }
                logger.LogInformation($"Finish processing page: {page}");
                page++;
            }
          
            return filmsToCreate;
        }

        private FilmCreateDto MapToFilmCreateDto(TmdbMovieDetails details)
        {
            var director = details.Credits.Crew.FirstOrDefault(c => c.Job == "Director")?.Name ?? "Unknown";
            var writers = string.Join(", ", details.Credits.Crew
                .Where(c => c.Job == "Writer" || c.Job == "Screenplay" || c.Job == "Author")
                .Select(c => c.Name).Distinct().Take(3));
            var cast = string.Join(", ", details.Credits.Cast.Take(5).Select(c => c.Name));

            var country = details.ProductionCountries.FirstOrDefault()?.Name ?? "Unknown";
            return new FilmCreateDto
            {
                Title = details.Title,
                Description = details.Overview,
                ReleaseDate = DateOnly.TryParse(details.ReleaseDate, out var date) ? date : default,
                ReleaseYear = DateOnly.TryParse(details.ReleaseDate, out var year) ? year.Year : 0,
                ImageUrl = !string.IsNullOrEmpty(details.PosterPath) ? $"https://image.tmdb.org/t/p/w500{details.PosterPath}" : null,
                FilmDirector = !string.IsNullOrEmpty(director) ? director : "Unknown",
                FilmWriters = !string.IsNullOrEmpty(writers) ? writers : "Unknown",
                FilmMainCast = !string.IsNullOrEmpty(cast) ? cast : "Unknown",
                FilmBudget = (int)details.Budget,
                FilmBoxOffice = (int)details.Revenue,
                FilmRuntimeInMinutes = details.Runtime,
                FilmCountryOfOrigin = !string.IsNullOrEmpty(country) ? country : "Unknown",
                FilmGenres = MapGenres(details.Genres),
                ExternalId = $"tmdb-{details.Id}"
            };
        }

        private List<FilmGenre> MapGenres(List<TmdbGenre> tmdbGenres)
        {
            var result = new List<FilmGenre>();
            if(tmdbGenres == null || !tmdbGenres.Any())
            {
                logger.LogInformation("No genres found for the movie, assigning 'Other' genre.");
                return new List<FilmGenre> { FilmGenre.Other };
            }
            foreach (var gente in tmdbGenres)
            {
                if(GenreMap.genreMap.TryGetValue(gente.Id, out var filmGenre))
                {
                    result.Add(filmGenre);
                }
                else
                {
                    logger.LogInformation("Genre '{GenreName}' not found in mapping, skipping.", gente.Name);
                }
            }
            return result.Any() ? result.Distinct().ToList() : new List<FilmGenre> { FilmGenre.Other };
        }
    }
}  