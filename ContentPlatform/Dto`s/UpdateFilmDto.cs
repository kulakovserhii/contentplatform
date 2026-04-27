using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class UpdateFilmDto: UpdateContentDto
    {
        public string? FilmDirector { get; set; }
        public string? FilmWriters { get; set; }
        public string? FilmMainCast { get; set; }
        public int? FilmBudget { get; set; }
        public int? FilmBoxOffice { get; set; }
        public int? FilmRuntimeInMinutes { get; set; }
        public string? FilmCountryOfOrigin { get; set; }
        public string? FilmAwards { get; set; }
        public List<FilmGenre>? FilmGenres { get; set; }
    }
}
