using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbTvResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("overview")]
        public string Overview { get; set; } = "";
        [JsonPropertyName("first_air_date")]
        public string? FirstAirDate { get; set; }
        [JsonPropertyName("last_air_date")]
        public string? LastAirDate { get; set; }
        [JsonPropertyName("number_of_seasons")]
        public int TotalSeasons { get; set; }
        [JsonPropertyName("number_of_episodes")]
        public int TotalEpisodes { get; set; }
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set;}
        [JsonPropertyName("genres")]
        public List<TmdbGenre> Genres { get; set; }
        [JsonPropertyName("created_by")]
        public List<TmdbCreator>? CreatedBy { get; set; }
        [JsonPropertyName("networks")] 
        public List<TmdbNetwork>? Networks { get; set; }
        [JsonPropertyName("credits")]
        public TmdbCredits? Credits { get; set; }
    }
}
