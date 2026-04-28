using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.IgdbModels
{
    public class IgdbPlatform
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
