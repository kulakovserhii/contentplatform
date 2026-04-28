using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbNetwork
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
