namespace ContentPlatform.Dto_s
{
    public class ContentSmallInfo
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int ReleaseYear { get; set; }
        public double AverageRating { get; set; }
        public int NumberOfRatings { get; set; }
        public string? ImageUrl { get; set; }
    }
}
