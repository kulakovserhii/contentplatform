using ContentPlatform.Enums;

namespace ContentPlatform.Models
{
    public class Book: Content
    {
        public required string Author { get; set; }
        public string Publisher { get; set; }
        public required string OriginalLanguage { get; set; }
        public int Pages { get; set; }
        public List<BookGenre> Genres { get; set; } = new List<BookGenre>();
    }
}
