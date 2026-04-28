using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbEpisodeDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("overview")]
        public string Overview { get; set; } = "";
        [JsonPropertyName("air_date")]
        public string? AirDate { get; set; }
        [JsonPropertyName("episode_number")]
        public int EpisodeNumber { get; set; }
        [JsonPropertyName("season_number")]
        public int SeasonNumber { get; set; }
        [JsonPropertyName("runtime")]
        public int? Runtime { get; set;}
        [JsonPropertyName("still_path")]
        public string? StillPath { get; set; }
    }
}
