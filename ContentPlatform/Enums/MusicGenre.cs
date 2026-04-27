using System.Text.Json.Serialization;

namespace ContentPlatform.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MusicGenre
    {
        Rap,
        Rock,
        Pop,
        Jazz,
        Classical,
        Country,
        Electronic,
        HipHop,
        RnB,
        Reggae,
        Blues,
        Metal,
        Folk,
        Other,
    }
}
