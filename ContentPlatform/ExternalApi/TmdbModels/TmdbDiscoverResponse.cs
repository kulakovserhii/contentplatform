using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbDiscoverResponse
    {
        [JsonPropertyName("results")]
        public List<TmdbMovieResult> Results { get; set; } = new();

    }
}
