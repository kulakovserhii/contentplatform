using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.IgdbModels
{
    public class IgdbGameResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("summary")]
        public string Summary { get; set; } = "";
        [JsonPropertyName("first_release_date")]
        public long? FirstReleaseDate { get; set; }
        [JsonPropertyName("cover")]
        public IgdbCover? Cover { get; set; }
        [JsonPropertyName("involved_companies")]
        public List<IgdbInvolvedCompany>? InvolvedCompanies = new();
        [JsonPropertyName("platforms")]
        public List<IgdbPlatform>? Platforms { get; set; }
        [JsonPropertyName("genres")]
        public List<IgdbGenre>? Genres { get; set; }
    }
}
