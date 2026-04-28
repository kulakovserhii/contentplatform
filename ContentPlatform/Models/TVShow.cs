using ContentPlatform.Enums;

namespace ContentPlatform.Models
{
    public class TVShow : Content
    {
        public string Creators { get; set; }
        public string Director { get; set; }
        public string MainCast { get; set; }
        public int TotalSeasons { get; set; }
        public int TotalEpisodes { get; set; }
        public string? Networks { get; set; }
        public DateTime? EndDate { get; set; }
        public List<FilmGenre> Genres { get; set; } = new List<FilmGenre>();
        public List<Episode> Episodes { get; set; } = new List<Episode>();
    }
}