using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.IgdbModels
{
    public class IgdbGenre
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
