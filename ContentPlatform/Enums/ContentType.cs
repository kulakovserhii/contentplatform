using System.Text.Json.Serialization;

namespace ContentPlatform.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContentType
    {
        Film, 
        TVShow,
        Music,
        Episode,
        Game,
        Book,
    }
}
