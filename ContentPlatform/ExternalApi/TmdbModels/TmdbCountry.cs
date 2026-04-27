using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbCountry
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
