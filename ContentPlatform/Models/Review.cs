using System.ComponentModel.DataAnnotations;

namespace ContentPlatform.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Range(1, 10)]
        public int Rating { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set;}
        public int UserId { get; set; }
        public User User { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}
