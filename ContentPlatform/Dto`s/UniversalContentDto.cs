using ContentPlatform.Enums;
using ContentPlatform.Models;

namespace ContentPlatform.Dto_s
{
    public class UniversalContentDto
    {
        public ContentType ContentType { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleaseYear { get; set; }
        public double AverageRating { get; set; }
        public int NumberOfRatings { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? FilmDirector { get; set; }
        public string? FilmWriters { get; set; }
        public string? FilmMainCast { get; set; }
        public int? FilmBudget { get; set; }
        public int? FilmBoxOffice { get; set; }
        public int? FilmRuntimeInMinutes { get; set; }
        public string? FilmCountryOfOrigin { get; set; }
        public string? FilmAwards { get; set; }
        public List<FilmGenre>? FilmGenres { get; set; }
        public int? TVShowId { get; set; }
        public int? EpisodeSeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
        public int? EpisodesTotalNumber { get; set; }
        public int? EpisodeRuntimeInMinutes { get; set; }
        public string? GameDeveloper { get; set; }
        public string? GamePublisher { get; set; }
        public List<Platform>? GamePlatforms { get; set; } = new List<Platform>();
        public List<GameGenre>? GameGenres { get; set; } = new List<GameGenre>();
        public string? MusicArtist { get; set; }
        public string? MusicAlbum { get; set; }
        public int? MusicDurationInSeconds { get; set; }
        public string? MusicLabel { get; set; }
        public string? MusicLanquage { get; set; }
        public List<MusicGenre>? MusciGenres { get; set; } = new List<MusicGenre>();
        public string? TVShowCreators { get; set; }
        public string? TVShowDirector { get; set; }
        public string? TVShowMainCast { get; set; }
        public int? TVShowTotalSeasons { get; set; }
        public int? TVShowTotalEpisodes { get; set; }
        public string? TVShowNetworks { get; set; }
        public DateTime? TVShowEndDate { get; set; }
        public List<FilmGenre>? TVShowGenres { get; set; } = new List<FilmGenre>();
        public required string BookAuthor { get; set; }
        public string BookPublisher { get; set; }
        public required string BookOriginalLanguage { get; set; }
        public int BookPages { get; set; }
        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}

