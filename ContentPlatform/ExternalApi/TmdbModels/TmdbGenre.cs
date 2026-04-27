using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbGenre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

    }
}
