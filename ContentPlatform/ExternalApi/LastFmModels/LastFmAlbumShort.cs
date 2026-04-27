using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmAlbumShort
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
    }
}
