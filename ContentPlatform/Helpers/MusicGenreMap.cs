using ContentPlatform.Enums;

namespace ContentPlatform.Helpers
{
    public static class MusicGenreMap
    {
        public static readonly Dictionary<string, MusicGenre> Map = new(StringComparer.OrdinalIgnoreCase)
        {
            { "rap", MusicGenre.Rap },
            { "hip-hop", MusicGenre.HipHop },
            { "hip hop", MusicGenre.HipHop },
            { "trap", MusicGenre.Rap },
            { "rock", MusicGenre.Rock },
            { "alternative rock", MusicGenre.Rock },
            { "indie rock", MusicGenre.Rock },
            { "metal", MusicGenre.Metal },
            { "heavy metal", MusicGenre.Metal },
            { "hard rock", MusicGenre.Rock },
            { "pop", MusicGenre.Pop },
            { "dance pop", MusicGenre.Pop },
            { "k-pop", MusicGenre.Pop },
            { "electronic", MusicGenre.Electronic },
            { "techno", MusicGenre.Electronic },
            { "house", MusicGenre.Electronic },
            { "ambient", MusicGenre.Electronic },
            { "jazz", MusicGenre.Jazz },
            { "classical", MusicGenre.Classical },
            { "country", MusicGenre.Country },
            { "rnb", MusicGenre.RnB },
            { "r&b", MusicGenre.RnB },
            { "reggae", MusicGenre.Reggae },
            { "blues", MusicGenre.Blues },
            { "folk", MusicGenre.Folk }
        };
    }
}
