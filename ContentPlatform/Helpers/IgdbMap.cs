using ContentPlatform.Enums;

namespace ContentPlatform.Helpers
{
    public static class IgdbMap
    {
        public static readonly Dictionary<string, GameGenre> GenreMap = new(StringComparer.OrdinalIgnoreCase)
        {
             { "Role-playing (RPG)", GameGenre.RPG },
            { "Shooter", GameGenre.Shooter },
            { "Adventure", GameGenre.Adventure },
            { "Action", GameGenre.Action },
            { "Strategy", GameGenre.Strategy },
            { "Simulator", GameGenre.Simulation },
            { "Sport", GameGenre.Sports },
            { "Racing", GameGenre.Racing },
            { "Puzzle", GameGenre.Puzzle },
            { "Fighting", GameGenre.Fighting },
            { "Platform", GameGenre.Platformer },
            { "MOBA", GameGenre.MMO }
        };

        public static readonly Dictionary<string, Platform> PlatformMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "PC (Microsoft Windows)", Platform.PC },
            { "PlayStation 5", Platform.PS5 },
            { "PlayStation 4", Platform.PS4 },
            { "Xbox Series X|S", Platform.XBox },
            { "Xbox One", Platform.XBox },
            { "Nintendo Switch", Platform.NintendoSwitch },
            { "Android", Platform.Mobile },
            { "iOS", Platform.Mobile }
        };
    }
}
