using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class MusicCreateDto: ContentCreateDto
    {
        public string? MusicArtist { get; set; }
        public string? MusicAlbum { get; set; }
        public int? MusicDurationInSeconds { get; set; }
        public string? MusicLabel { get; set; }
        public string? MusicLanquage { get; set; }
        public List<MusicGenre>? MusciGenres { get; set; }
    }
}
