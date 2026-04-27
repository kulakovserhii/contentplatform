using ContentPlatform.Enums;

namespace ContentPlatform.Helpers
{
    public static class GenreMap
    {
        public static readonly Dictionary<int, FilmGenre> genreMap = new Dictionary<int, FilmGenre>
        {
            { 28, FilmGenre.Action },
            { 12, FilmGenre.Adventure },
            { 16, FilmGenre.Animation },
            { 35, FilmGenre.Comedy },
            { 80, FilmGenre.Crime },
            { 99, FilmGenre.Documentary },
            { 18, FilmGenre.Drama },
            { 10751, FilmGenre.Fantasy },
            { 14, FilmGenre.Fantasy },
            { 36, FilmGenre.Historical },
            { 27, FilmGenre.Horror },
            { 10402, FilmGenre.Musical }, 
            { 9648, FilmGenre.Mystery },
            { 10749, FilmGenre.Romance },
            { 878, FilmGenre.ScienceFiction },
            { 53, FilmGenre.Thriller },
            { 10752, FilmGenre.Historical }, 
            { 37, FilmGenre.Western }
        };
    }
}
