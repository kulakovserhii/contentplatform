using ContentPlatform.Enums;

namespace ContentPlatform.Models
{
    public class Game: Content
    {
        public required string Developer { get; set; }
        public required string Publisher { get; set; }
        public List<Platform> Platforms { get; set; } = new List<Platform>();
        public List<GameGenre> Genres { get; set; } = new List<GameGenre>();
    }
}
