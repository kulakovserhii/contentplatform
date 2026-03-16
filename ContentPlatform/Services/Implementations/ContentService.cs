using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Models;
using AutoMapper;
using ContentPlatform.Services.Interfaces;
namespace ContentPlatform.Services.Implementations
{
    public class ContentService(IContentRepository contentRepository, IMapper mapper) : IContentService
    {
        public async Task<Content> CreateContentAsync(UniversalContentDto universalContentDto)
        {
            universalContentDto.AverageRating = 0;
            universalContentDto.NumberOfRatings = 0;
            universalContentDto.CreatedAt = DateTime.UtcNow;
            Content content;
            switch (universalContentDto.ContentType)
            {
                case ContentType.Film:
                    content = CreateFilm(universalContentDto);
                    break;
                case ContentType.TVShow:
                    content = CreateTVShow(universalContentDto);
                    break;
                case ContentType.Music:
                    content = CreateMusic(universalContentDto);
                    break;
                case ContentType.Episode:
                    content = await CreateEpisode(universalContentDto);
                    break;
                case ContentType.Game:
                    content = CreateGame(universalContentDto);
                    break;
                case ContentType.Book:
                    content = CreateBook(universalContentDto);
                    break;
                default:
                    throw new Exception("Invalid content type");
            }
            await contentRepository.CreateContentAsync(content);
            return content;
        }

        public Task<List<Content>> GetAllContentByTypeAsync(ContentType contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<ContentDetailsDto> GetContentByIdAsync(int contentId)
        {
            var content = await contentRepository.GetContentWithReviewAsync(contentId);
            if (content == null)
                return null;
            return mapper.Map<ContentDetailsDto>(content);
        }
        private bool ContentValidator(UniversalContentDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
                return false;
            if (string.IsNullOrEmpty(dto.Description))
                return false;
            if (dto.ReleaseYear == default)
                return false;
            return true;
        }
        private Film CreateFilm(UniversalContentDto dto)
        {
            if (!ContentValidator(dto))
                throw new Exception("Title, descriprtion and release date must be filled");
            if (string.IsNullOrEmpty(dto.FilmDirector))
                throw new Exception("Film director field must be filled");
            if (string.IsNullOrEmpty(dto.FilmWriters))
                throw new Exception("Film writers field must be filled");
            if (string.IsNullOrEmpty(dto.FilmMainCast))
                throw new Exception("Film main cast field must be filled");
            if (dto.FilmRuntimeInMinutes == null || dto.FilmRuntimeInMinutes <=0 )
                throw new Exception("Film runtime in minutes field must be filled and greater than 0");
            if (dto.FilmGenres.Count < 1)
                throw new Exception("Film genres field must be filled with at least one genre");
            return new Film
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseYear = dto.ReleaseYear,
                ReleaseDate = dto.ReleaseDate,
                ImageUrl = dto.ImageUrl,
                Director = dto.FilmDirector!,
                Writers = dto.FilmWriters!,
                MainCast = dto.FilmMainCast!,
                Budget = dto.FilmBudget ?? 0,
                BoxOffice = dto.FilmBoxOffice ?? 0,
                RuntimeInMinutes = dto.FilmRuntimeInMinutes ?? 0,
                CountryOfOrigin = dto.FilmCountryOfOrigin,
                Awards = dto.FilmAwards,
                Genres = dto.FilmGenres,
            };
        }
        private TVShow CreateTVShow(UniversalContentDto dto)
        {
            if (!ContentValidator(dto))
                throw new Exception("Title, descriprtion and release date must be filled");
            if(string.IsNullOrEmpty(dto.TVShowDirector))
                throw new Exception("TV show director field must be filled");
            if(string.IsNullOrEmpty(dto.TVShowCreators))
                throw new Exception("TV show creators field must be filled");
            if(string.IsNullOrEmpty(dto.TVShowMainCast))
                throw new Exception("TV show main cast field must be filled");
            if(string.IsNullOrEmpty(dto.TVShowNetworks))
                throw new Exception("TV show networks field must be filled");
            if(dto.TVShowTotalSeasons == null || dto.TVShowTotalSeasons <= 0)
                throw new Exception("TV show total seasons field must be filled and greater than 0");
            if(dto.TVShowTotalEpisodes == null || dto.TVShowTotalEpisodes <= 0)
                throw new Exception("TV show total episodes field must be filled and greater than 0");
            if(dto.TVShowGenres.Count < 1)
                throw new Exception("TV show genres field must be filled with at least one genre");
            return new TVShow
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseYear = dto.ReleaseYear,
                ReleaseDate = dto.ReleaseDate,
                ImageUrl = dto.ImageUrl,
                Director = dto.TVShowDirector!,
                Creators = dto.TVShowCreators!,
                MainCast = dto.TVShowMainCast!,
                Networks = dto.TVShowNetworks!,
                TotalSeasons = dto.TVShowTotalSeasons ?? 0,
                TotalEpisodes = dto.TVShowTotalEpisodes ?? 0,
                Genres = dto.TVShowGenres,
                EndDate = dto.TVShowEndDate ?? null,
            };
        }
        private Music CreateMusic(UniversalContentDto dto)
        {
            if (!ContentValidator(dto))
                throw new Exception("Title, descriprtion and release date must be filled");
            if(string.IsNullOrEmpty(dto.MusicArtist))
                throw new Exception("Music artist field must be filled");
            if(dto.MusicDurationInSeconds == null || dto.MusicDurationInSeconds <= 0)
                throw new Exception("Music duration in seconds field must be filled and greater than 0");
            if(string.IsNullOrEmpty(dto.MusicLanquage))
                throw new Exception("Music language field must be filled");
            if(dto.MusciGenres.Count < 1)
                throw new Exception("Music genres field must be filled with at least one genre");
            return new Music
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseYear = dto.ReleaseYear,
                ReleaseDate = dto.ReleaseDate,
                ImageUrl = dto.ImageUrl,
                Artist = dto.MusicArtist!,
                Album = dto.MusicAlbum ?? "Single",
                DurationInSeconds = dto.MusicDurationInSeconds ?? 0,
                Label = dto.MusicLabel ?? "No label",
                Lanquage = dto.MusicLanquage,
                Genres = dto.MusciGenres,
            };
        }
        private async Task<Episode> CreateEpisode(UniversalContentDto dto)
        {
            if(!ContentValidator(dto))
                throw new Exception("Title, descriprtion and release date must be filled");
            if(dto.TVShowId == null || dto.TVShowId <= 0)
                throw new Exception("TV show id field must be filled and greater than 0");
            if(dto.EpisodeSeasonNumber == null || dto.EpisodeSeasonNumber <= 0)
                throw new Exception("Episode season number field must be filled and greater than 0");
            if(dto.EpisodeNumber == null || dto.EpisodeNumber <= 0)
                throw new Exception("Episode number field must be filled and greater than 0");
            if(dto.EpisodesTotalNumber == null || dto.EpisodesTotalNumber <= 0)
                throw new Exception("Episode total number field must be filled and greater than 0");
            if(dto.EpisodeRuntimeInMinutes == null || dto.EpisodeRuntimeInMinutes <= 0)
                throw new Exception("Episode runtime in minutes field must be filled and greater than 0");
            var episode = new Episode
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseYear = dto.ReleaseYear,
                ReleaseDate = dto.ReleaseDate,
                ImageUrl = dto.ImageUrl,
                TVShowId = dto.TVShowId.Value,
                SeasonNumber = dto.EpisodeSeasonNumber ?? 0,
                EpisodeNumber = dto.EpisodeNumber ?? 0,
                TotalNumber = dto.EpisodesTotalNumber ?? 0,
                RuntimeInMinutes = dto.EpisodeRuntimeInMinutes ?? 0,
            };
            var TVShow = await contentRepository.GetContentByIdAsync<TVShow>(dto.TVShowId!.Value);
            if(TVShow == null)
                throw new Exception("TV show with the given id does not exist");
            return episode;
        }   
        private Game CreateGame(UniversalContentDto dto)
        {
            if(!ContentValidator(dto))
                throw new Exception("Title, descriprtion and release date must be filled");
            if(string.IsNullOrEmpty(dto.GameDeveloper))
                throw new Exception("Game developer field must be filled");
            if(string.IsNullOrEmpty(dto.GamePublisher))
                throw new Exception("Game publisher field must be filled");
            if(dto.GamePlatforms.Count < 1)
                throw new Exception("Game platforms field must be filled with at least one platform");
            if(dto.GameGenres.Count < 1)
                throw new Exception("Game genres field must be filled with at least one genre");
            return new Game
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseYear = dto.ReleaseYear,
                ReleaseDate = dto.ReleaseDate,
                ImageUrl = dto.ImageUrl,
                Developer = dto.GameDeveloper!,
                Publisher = dto.GamePublisher!,
                Platforms = dto.GamePlatforms,
                Genres = dto.GameGenres,
            };
        }
        private Book CreateBook(UniversalContentDto dto)
        {
            if(!ContentValidator(dto))
                throw new Exception("Title, descriprtion and release date must be filled");
            if(string.IsNullOrEmpty(dto.BookAuthor))
                throw new Exception("Book author field must be filled");
            if(string.IsNullOrEmpty(dto.BookOriginalLanguage))
                throw new Exception("Book original language field must be filled");
            if(dto.BookPages <= 0)
                throw new Exception("Book pages field must be greater than 0");
            if(dto.BookGenres.Count < 1)
                throw new Exception("Book genres field must be filled with at least one genre");
            return new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseYear = dto.ReleaseYear,
                ReleaseDate = dto.ReleaseDate,
                ImageUrl = dto.ImageUrl,
                Author = dto.BookAuthor!,
                Publisher = dto.BookPublisher ?? "No publisher",
                OriginalLanguage = dto.BookOriginalLanguage,
                Pages = dto.BookPages,
                Genres = dto.BookGenres,
            };
        }
    }
}
