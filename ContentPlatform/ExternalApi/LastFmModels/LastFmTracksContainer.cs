using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmTracksContainer
    {
        [JsonPropertyName("track")]
        public List<LastFmTrackShort> TrackList { get; set; } = new();
    }
}
