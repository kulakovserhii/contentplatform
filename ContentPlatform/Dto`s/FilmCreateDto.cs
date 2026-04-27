using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class FilmCreateDto: ContentCreateDto
    {
        public required string FilmDirector { get; set; }
        public string FilmWriters { get; set; }
        public required string FilmMainCast { get; set; }
        public int? FilmBudget { get; set; }
        public int? FilmBoxOffice { get; set; }
        public int? FilmRuntimeInMinutes { get; set; }
        public string? FilmCountryOfOrigin { get; set; }
        public string? FilmAwards { get; set; }
        public List<FilmGenre>? FilmGenres { get; set; }
        public string? ExternalId { get; set; }
    }
}
