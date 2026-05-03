namespace ContentPlatform.Dto_s
{
    public class AchievementDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public required string Description { get; set; }
        public string? BadgeTitle { get; set; }
    }
}
