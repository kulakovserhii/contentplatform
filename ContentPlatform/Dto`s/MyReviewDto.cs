namespace ContentPlatform.Dto_s
{
    public class MyReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set;}
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } 
        public MyReviewContentDto Content { get; set; }
    }
}
