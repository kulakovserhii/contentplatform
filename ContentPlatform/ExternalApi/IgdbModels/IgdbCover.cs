using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.IgdbModels
{
    public class IgdbCover
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
