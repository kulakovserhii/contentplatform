using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmTopTracksResponse
    {
        [JsonPropertyName("tracks")]
        public LastFmTracksContainer Tracks { get; set; } = new();

    }
}
