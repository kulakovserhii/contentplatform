using ContentPlatform.Enums;

namespace ContentPlatform.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public required string Description { get; set; }
        public string? BadgeTitle { get; set; }
        public AchievementCategory Category { get; set; }
        public int RequirementValue { get; set; }
    }
}
