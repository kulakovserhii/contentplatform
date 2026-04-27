using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class GameCreateDto: ContentCreateDto
    {
        public string? GameDeveloper { get; set; }
        public string? GamePublisher { get; set; }
        public List<Platform>? GamePlatforms { get; set; }
        public List<GameGenre>? GameGenres { get; set; } 
    }
}
