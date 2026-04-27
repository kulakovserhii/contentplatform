using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmTrackDetailsResponse
    {
        [JsonPropertyName("track")]
        public LastFmTrackDetails Track { get; set; } = new();
    }
}
