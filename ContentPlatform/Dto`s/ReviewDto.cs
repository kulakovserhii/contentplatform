using ContentPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace ContentPlatform.Dto_s
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int LikeCount { get; set; } 
        public int DislikeCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto User { get; set; }
    }
}
