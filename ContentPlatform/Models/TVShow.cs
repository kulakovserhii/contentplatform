using ContentPlatform.Enums;

namespace ContentPlatform.Models
{
    public class TVShow : Content
    {
        public required string Creators { get; set; }
        public required string Director { get; set; }
        public required string MainCast { get; set; }
        public int TotalSeasons { get; set; }
        public int TotalEpisodes { get; set; }
        public string? Networks { get; set; }
        public DateTime? EndDate { get; set; }
        public List<FilmGenre> Genres { get; set; } = new List<FilmGenre>();
        public List<Episode> Episodes { get; set; } = new List<Episode>();
    }
}