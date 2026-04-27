using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbMovieResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}

