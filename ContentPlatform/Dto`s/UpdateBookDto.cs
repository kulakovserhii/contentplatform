using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class UpdateBookDto: UpdateContentDto
    {
        public string? BookAuthor { get; set; }
        public string? BookPublisher { get; set; }
        public string? BookOriginalLanguage { get; set; }
        public int? BookPages { get; set; }
        public List<BookGenre>? BookGenres { get; set; }
    }
}
