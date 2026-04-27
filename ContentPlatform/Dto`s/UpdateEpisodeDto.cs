namespace ContentPlatform.Dto_s
{
    public class UpdateEpisodeDto: UpdateContentDto
    {
        public int? EpisodeTVShowId { get; set; }
        public int? EpisodeSeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
        public int? EpisodesTotalNumber { get; set; }
        public int? EpisodeRuntimeInMinutes { get; set; }
    }
}
