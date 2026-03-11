using System.Text.Json.Serialization;

namespace ContentPlatform.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GameGenre
    {
        RPG,
        Action,
        Strategy,
        Sandbox,
        Horror,
        Simulation,
        Sports,
        Racing,
        Puzzle,
        Adventure,
        Fighting,
        Platformer,
        Shooter,
        MMO,
    }
}
