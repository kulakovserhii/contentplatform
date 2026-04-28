namespace ContentPlatform.Models
{
    public class UserAchevement
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Achievement Achievement { get; set; }
        public int AchievementId { get; set; }
        public DateTime UnlockedAt { get; set; }
    }
}
