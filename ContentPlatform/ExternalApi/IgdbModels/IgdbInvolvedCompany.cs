using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.IgdbModels
{
    public class IgdbInvolvedCompany
    {
        [JsonPropertyName("developer")]
        public bool IsDeveloper { get; set; }
        [JsonPropertyName("publisher")]
        public bool IsPublisher { get; set; }
        [JsonPropertyName("company")]
        public IgdbCompany Company { get; set; }
    }
}
