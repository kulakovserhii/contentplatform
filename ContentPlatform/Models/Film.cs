using ContentPlatform.Enums;

namespace ContentPlatform.Models
{
    public class Film: Content
    {
        public required string Director { get; set; }
        public required string Writers { get; set; }
        public required string MainCast { get; set; }
        public int Budget { get; set; }
        public int BoxOffice { get; set; }
        public int RuntimeInMinutes { get; set; }
        public string? CountryOfOrigin { get; set; }
        public string? Awards { get; set; }
        public List<FilmGenre> Genres { get; set; }
    }
}
