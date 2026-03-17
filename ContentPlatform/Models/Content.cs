namespace ContentPlatform.Models
{
    public abstract class Content
    {
        public int Id { get; set; }
        public required string Title { get; set; } 
        public required string Description { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int ReleaseYear { get; set; }
        public double AverageRating { get; set; }
        public int NumberOfRatings { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
