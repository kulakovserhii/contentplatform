namespace ContentPlatform.Models
{
    public class Episode: Content
    {
        public TVShow TVShow { get; set; }
        public int TVShowId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public int TotalNumber { get; set; }
        public int RuntimeInMinutes { get; set; }
    }
}
