using System.Text.Json.Serialization;

namespace ContentPlatform.ExternalApi.IgdbModels
{
    public class IgdbAuthResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = "";
        [JsonPropertyName("expires_in")]
        public int ExipresIn { get; set; }
    }
}
