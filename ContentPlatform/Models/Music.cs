using ContentPlatform.Enums;

namespace ContentPlatform.Models
{
    public class Music: Content
    {
        public required string Artist { get; set; }
        public string Album { get; set; }
        public int DurationInSeconds { get; set; }
        public string Label { get; set; }
        public required string Lanquage { get; set; }
        public List<MusicGenre> Genres { get; set; } = new List<MusicGenre>();
    }
}
