using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.TmdbModels
{
    public class TmdbTvListResponse
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("results")]
        public List<TmdbTvResult>? Results { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set;}
        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}
