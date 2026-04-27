using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmTagsContainer
    {
        [JsonPropertyName("tag")]
        public List<LastFmTag> Tags { get; set; } = new();
    }
}
