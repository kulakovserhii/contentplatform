namespace ContentPlatform.Dto_s
{
    public class EpisodeCreateDto: ContentCreateDto
    {
        public string? ExternalId { get; set; }
        public int? EpisodeTVShowId { get; set; }
        public int? EpisodeSeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
        public int? EpisodesTotalNumber { get; set; }
        public int? EpisodeRuntimeInMinutes { get; set; }
    }
}
