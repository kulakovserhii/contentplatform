using ContentPlatform.Enums;

namespace ContentPlatform.Helpers
{
    public static class BookGenreMap
    {
        public static readonly Dictionary<string, BookGenre> Map = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Romance", BookGenre.Romance },
            { "Fantasy", BookGenre.Fantasy },
            { "Science Fiction", BookGenre.ScienceFiction },
            { "Sci-Fi", BookGenre.ScienceFiction },
            { "Detective", BookGenre.Detective },
            { "Mystery", BookGenre.Detective },
            { "Biography", BookGenre.Biography },
            { "Autobiography", BookGenre.Biography },
            { "Historical", BookGenre.Historical },
            { "History", BookGenre.Historical },
            { "Thriller", BookGenre.Thriller },
            { "Horror", BookGenre.Horror },
            { "Adventure", BookGenre.Adventure }
        };

        public static readonly Dictionary<BookGenre, string[]> Keywords = new()
        {
            { BookGenre.Romance, new[] { "romance", "love", "dating", "relationships" } },
            { BookGenre.Fantasy, new[] { "fantasy", "magic", "tales", "wizards", "dragons", "mythology" } },
            { BookGenre.ScienceFiction, new[] { "science fiction", "sci-fi", "space", "future", "robots", "dystopian" } },
            { BookGenre.Detective, new[] { "detective", "mystery", "crime", "investigation", "police" } },
            { BookGenre.Biography, new[] { "biography", "autobiography", "memoirs", "lives" } },
            { BookGenre.Historical, new[] { "historical", "history", "ancient", "war", "middle ages" } },
            { BookGenre.Thriller, new[] { "thriller", "suspense", "psychological" } },
            { BookGenre.Horror, new[] { "horror", "ghosts", "supernatural", "vampires" } },
            { BookGenre.Adventure, new[] { "adventure", "survival", "exploration", "sea stories" } }
        };
    }
}