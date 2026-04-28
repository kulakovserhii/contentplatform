using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.OpenLibrary
{
    public class OpenLibraryResponse
    {
        [JsonPropertyName("docs")]
        public List<OpenLibraryDoc>? Docs { get; set; }
    }
}
