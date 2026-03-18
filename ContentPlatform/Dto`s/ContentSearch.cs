using ContentPlatform.Enums;
using ContentPlatform.Helpers;

namespace ContentPlatform.Dto_s
{
    public class ContentSearch
    {
        public List<ContentType>? ContentTypes { get; set; }
        public string? ContentName { get; set; }
        public int? YearFrom { get; set;}
        public int? YearTo { get; set; }
        public double? MinRating { get; set; }
        public SortBy? SortBy { get; set; }
        public SortType? SortType { get; set; }
        public List<FilmGenre>? FilmGenres { get; set; }
        public List<FilmGenre>? TVShowGenres { get; set; }
        public List<MusicGenre>? MusicGenres { get; set; }
        public List<GameGenre>? GameGenres { get; set; }
        public List<BookGenre>? BookGenres { get; set; }
    }
}
