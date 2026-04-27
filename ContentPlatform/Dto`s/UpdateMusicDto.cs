using ContentPlatform.Enums;

namespace ContentPlatform.Dto_s
{
    public class UpdateMusicDto: UpdateContentDto
    {
        public string? MusicArtist { get; set; }
        public string? MusicAlbum { get; set; }
        public int? MusicDurationInSeconds { get; set; }
        public string? MusicLabel { get; set; }
        public string? MusicLanguage { get; set; }
        public List<MusicGenre>? MusicGenres { get; set; }
    }
}
