using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class BookCreateDto: ContentCreateDto
    {
        public required string BookAuthor { get; set; }
        public string BookPublisher { get; set; }
        public required string BookOriginalLanguage { get; set; }
        public int BookPages { get; set; }
        public List<BookGenre> BookGenres { get; set; } = new();
        public string? ExternalId { get; set; }
    }
}
