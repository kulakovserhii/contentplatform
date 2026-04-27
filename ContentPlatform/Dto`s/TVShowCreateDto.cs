using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class TVShowCreateDto: ContentCreateDto
    {
        public string? TVShowCreators { get; set; }
        public string? TVShowDirector { get; set; }
        public string? TVShowMainCast { get; set; }
        public int? TVShowTotalSeasons { get; set; }
        public int? TVShowTotalEpisodes { get; set; }
        public string? TVShowNetworks { get; set; }
        public DateTime? TVShowEndDate { get; set; }
        public List<FilmGenre>? TVShowGenres { get; set; }
    }
}
