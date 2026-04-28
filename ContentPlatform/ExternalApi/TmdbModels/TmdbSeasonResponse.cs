using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbSeasonResponse
    {
        [JsonPropertyName("episodes")]
        public List<TmdbEpisodeDto>? Episodes { get; set; }
    }
}
