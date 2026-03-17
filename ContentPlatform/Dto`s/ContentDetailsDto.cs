namespace ContentPlatform.Dto_s
{
    public class ContentDetailsDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int ReleaseYear { get; set; }
        public double AverageRating { get; set; }
        public int NumberOfRatings { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
        public object? AdditionalDetails { get; set; }
    }
}
