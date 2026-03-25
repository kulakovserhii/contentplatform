using ContentPlatform.Enums;

namespace ContentPlatform.Models
{
    public class RateReview
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Review Review { get; set; }
        public int ReviewId { get; set; }
        public Evaluate VoteType { get; set; }
    }
}
