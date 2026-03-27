using System.Text.Json.Serialization;

namespace ContentPlatform.Helpers
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReviewSortBy
    {
        Popular, 
        CreatedAt,
    }
}
