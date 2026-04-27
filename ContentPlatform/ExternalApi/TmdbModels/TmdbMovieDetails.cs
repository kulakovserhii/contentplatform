using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbMovieDetails
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty;
        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;
        [JsonPropertyName("runtime")]
        public int Runtime { get; set; }
        [JsonPropertyName("budget")]
        public long Budget { get; set; }
        [JsonPropertyName("revenue")]
        public long Revenue { get; set; }
        [JsonPropertyName("poster_path")]
        public string ? PosterPath { get; set; }
        [JsonPropertyName("genres")]
        public List<TmdbGenre> Genres { get; set; } = new List<TmdbGenre>();

        [JsonPropertyName("credits")]
        public TmdbCredits Credits { get; set; } = new TmdbCredits();
        [JsonPropertyName("production_countries")]
        public List<TmdbCountry> ProductionCountries { get; set; } = new();
    }
}
