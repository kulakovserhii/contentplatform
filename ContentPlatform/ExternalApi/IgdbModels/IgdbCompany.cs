using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.IgdbModels
{
    public class IgdbCompany
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
