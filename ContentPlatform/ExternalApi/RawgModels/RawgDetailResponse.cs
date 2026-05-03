using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.RawgModels
{
    public class RawgDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [JsonPropertyName("description_raw")]
        public string? DescriprionRaw { get; set; }
        public DateTime? Released { get; set; }
        [JsonPropertyName("background_image")]
        public string? BackgroundImage { get; set; }
        public List<RawgEntity>? Developers { get; set; }
        public List<RawgEntity>? Publishers { get; set; }
        public List<RawgGenre> Genres { get; set; }
        [JsonPropertyName("platforms")]
        public List<RawgPlatformWrapper>? Platforms { get; set; }
    }
}
