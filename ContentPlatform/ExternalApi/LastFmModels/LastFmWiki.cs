using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmWiki
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;
    }
}
