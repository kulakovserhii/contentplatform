using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.OpenLibrary
{
    public class OpenLibraryDoc
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("author_name")]
        public List<string>? AuthorNames { get; set; }
        [JsonPropertyName("publisher")]
        public List<string>? Publishers { get; set; }
        [JsonPropertyName("number_of_pages_median")]
        public int? Pages { get; set; }
        [JsonPropertyName("first_publish_year")]
        public int? FirstPublishYear { get; set; }
        [JsonPropertyName("language")]
        public List<string>? Languages { get; set; }
        [JsonPropertyName("cover_i")]
        public int? CoverId { get; set; }
        [JsonPropertyName("subject")]
        public List<string>? Subject { get; set; }
    }
}
