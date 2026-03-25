namespace ContentPlatform.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int? Age { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public List<RateReview> RateReviews { get; set; } = new List<RateReview>();
    }
}
