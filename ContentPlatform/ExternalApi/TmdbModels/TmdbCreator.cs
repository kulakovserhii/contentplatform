using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbCreator
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
