using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmTrackDetails
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("duration")]
        public string Duration { get; set; } = "0";
        [JsonPropertyName("artist")]
        public LastFmArtistShort Artist { get; set; } = new();
        [JsonPropertyName("album")]
        public LastFmAlbumShort? Album { get; set; }
        [JsonPropertyName("toptags")]
        public LastFmTagsContainer Toptags { get; set; } = new();
        [JsonPropertyName("wiki")]
        public LastFmWiki? Wiki { get; set; }
    }
}
