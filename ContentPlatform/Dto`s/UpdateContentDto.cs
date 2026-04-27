using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class UpdateContentDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int? ReleaseYear { get; set; }
        public string? ImageUrl { get; set; }
    }
}
