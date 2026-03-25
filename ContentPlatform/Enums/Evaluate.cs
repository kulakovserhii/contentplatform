using System.Text.Json.Serialization;

namespace ContentPlatform.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Evaluate
    {
        Like,
        Dislike,
    }
}
