using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbCastMember
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
