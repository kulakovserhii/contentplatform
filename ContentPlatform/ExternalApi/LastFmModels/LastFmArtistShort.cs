using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.LastFmModels
{
    public class LastFmArtistShort
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
