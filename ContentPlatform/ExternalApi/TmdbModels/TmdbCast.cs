using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbCast
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
