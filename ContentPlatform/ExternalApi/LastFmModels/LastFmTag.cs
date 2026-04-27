using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmTag
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
