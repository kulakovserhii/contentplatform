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

        public Task<List<ContentDetailsDto>> GetAllContentByTypeAsync(ContentType contentType)
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

        public async Task<List<ContentDetailsDto>> GetAllContentWithoutReviewsAsync()
        {
            var contents = await contentRepository.GetAllContentAsync();
            if (contents == null)
                return null;
            var contentsWithoutReviews = new List<ContentDetailsDto>();
            foreach(var content in contents)
            {
                contentsWithoutReviews.Add(mapper.Map<ContentDetailsDto>(content));
            }
            return contentsWithoutReviews;
        }

        public async Task<string> DeleteContentAsync(int contentId)
        {
            var content = await contentRepository.GetContentByIdAsync<Content>(contentId);
            if (content == null)
                return string.Empty;
            var result = await contentRepository.DeleteContent<Content>(content);
            if (!result)
                return "Content deletion failed";
            return "Content deleted successfully";
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
        public async Task<ContentDetailsDto> UpdateContentAsync(int contentId, UpdateContentDto dto)
        {
            var content = await contentRepository.GetContentByIdAsync<Content>(contentId);
            if (content == null)
                return null;
           if(content.GetType().Name != dto.ContentType.ToString())
                throw new Exception($"Content type mismatch. Expexted {content.GetType().Name} but got {dto.ContentType}");
            if (!string.IsNullOrEmpty(dto.Title))
                content.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Description))
                content.Description = dto.Description;
            if (dto.ReleaseDate.HasValue)
                content.ReleaseDate = dto.ReleaseDate.Value;
            if (dto.ReleaseYear.HasValue)
                content.ReleaseYear = dto.ReleaseYear.Value;
            if (!string.IsNullOrEmpty(dto.ImageUrl))
                content.ImageUrl = dto.ImageUrl;
            switch (content)
            {
                case Film film:
                    UpdateFilm(film, dto);
                    break;
                case TVShow tvShow:
                    UpdateTVShow(tvShow, dto);
                    break;
                case Music music:
                    UpdateMusic(music, dto);
                    break;
                case Episode episode:
                    await UpdateEpisode(episode, dto);
                    break;
                case Game game:
                    UpdateGame(game, dto);
                    break;
                case Book book:
                    UpdateBook(book, dto);
                    break;
            }
            await contentRepository.UpdateContentAsync(content);
            return mapper.Map<ContentDetailsDto>(content);
        }
        private void UpdateFilm(Film film, UpdateContentDto dto)
        {
            if(!string.IsNullOrEmpty(dto.FilmDirector))
                film.Director = dto.FilmDirector;
            if(!string.IsNullOrEmpty(dto.FilmWriters))
                film.Writers = dto.FilmWriters;
            if(!string.IsNullOrEmpty(dto.FilmMainCast))
                film.MainCast = dto.FilmMainCast;
            if(dto.FilmBudget.HasValue && dto.FilmBudget.Value > 0)
                film.Budget = dto.FilmBudget.Value;
            if(dto.FilmBoxOffice.HasValue && dto.FilmBoxOffice.Value > 0)
                film.BoxOffice  = dto.FilmBoxOffice.Value;
            if(dto.FilmRuntimeInMinutes.HasValue && dto.FilmRuntimeInMinutes.Value > 0)
                film.RuntimeInMinutes = dto.FilmRuntimeInMinutes.Value;
            if(!string.IsNullOrEmpty(dto.FilmCountryOfOrigin))
                film.CountryOfOrigin = dto.FilmCountryOfOrigin;
            if(!string.IsNullOrEmpty(dto.FilmAwards))
                film.Awards = dto.FilmAwards;
            if(dto.FilmGenres.Count > 0)
                film.Genres = dto.FilmGenres;
        }
        private void UpdateTVShow(TVShow tvShow, UpdateContentDto dto)
        {
            if(!string.IsNullOrEmpty(dto.TVShowDirector))
                tvShow.Director = dto.TVShowDirector;
            if(!string.IsNullOrEmpty(dto.TVShowCreators))
                tvShow.Creators = dto.TVShowCreators;
            if(!string.IsNullOrEmpty(dto.TVShowMainCast))
                tvShow.MainCast = dto.TVShowMainCast;
            if(dto.TVShowTotalSeasons.HasValue && dto.TVShowTotalSeasons > 0)
                tvShow.TotalSeasons = dto.TVShowTotalSeasons.Value;
            if(dto.TVShowTotalEpisodes.HasValue && dto.TVShowTotalEpisodes > 0)
                tvShow.TotalEpisodes = dto.TVShowTotalEpisodes.Value;
            if(!string.IsNullOrEmpty(dto.TVShowNetworks))
                tvShow.Networks = dto.TVShowNetworks;
            if(dto.TVShowEndDate.HasValue)
                tvShow.EndDate = dto.TVShowEndDate.Value;
            if(dto.TVShowGenres.Count > 1)
                tvShow.Genres = dto.TVShowGenres;
        }
        private void UpdateMusic(Music music, UpdateContentDto dto)
        {
            if(!string.IsNullOrEmpty(dto.MusicArtist))
                music.Artist = dto.MusicArtist;
            if(!string.IsNullOrEmpty(dto.MusicAlbum))
                music.Album = dto.MusicAlbum;
            if(dto.MusicDurationInSeconds.HasValue && dto.MusicDurationInSeconds > 0)
                music.DurationInSeconds = dto.MusicDurationInSeconds.Value;
            if(!string.IsNullOrEmpty(dto.MusicLabel))
                music.Label = dto.MusicLabel;
            if (!string.IsNullOrEmpty(dto.MusicLanguage))
                music.Lanquage = dto.MusicLanguage;
            if(dto.MusicGenres.Count > 0)
                music.Genres = dto.MusicGenres;
        }
        private async Task UpdateEpisode(Episode episode, UpdateContentDto dto)
        {
            if(dto.EpisodeSeasonNumber.HasValue &&  dto.EpisodeSeasonNumber > 0)
                episode.SeasonNumber = dto.EpisodeSeasonNumber.Value;
            if (dto.TVShowId.HasValue && dto.TVShowId.Value != episode.TVShowId)
            {
                var tvShowExists = await contentRepository.GetContentByIdAsync<TVShow>(dto.TVShowId.Value);
                if (tvShowExists == null)
                    throw new Exception($"TV Show with Id {dto.TVShowId} does not exist");
                episode.TVShowId = dto.TVShowId.Value;
            }
            if (dto.EpisodeNumber.HasValue && dto.EpisodeNumber > 0)
                episode.EpisodeNumber = dto.EpisodeNumber.Value;
            if (dto.EpisodesTotalNumber.HasValue && dto.EpisodesTotalNumber > 0)
                episode.EpisodeNumber = dto.EpisodesTotalNumber.Value;
            if(dto.EpisodeRuntimeInMinutes.HasValue && dto.EpisodeRuntimeInMinutes > 0)
                episode.RuntimeInMinutes = dto.EpisodeRuntimeInMinutes.Value;
        }
        private void UpdateGame(Game game, UpdateContentDto dto)
        {
            if(!string.IsNullOrEmpty(dto.GamePublisher))
                game.Publisher = dto.GamePublisher;
            if(!string.IsNullOrEmpty(dto.GameDeveloper))
                game.Developer = dto.GameDeveloper;
            if(dto.GamePlatforms.Count > 0)
                game.Platforms = dto.GamePlatforms;
            if(dto.GameGenres.Count > 0)
                game.Genres = dto.GameGenres;
        }
        private void UpdateBook(Book book, UpdateContentDto dto)
        {
            if (!string.IsNullOrEmpty(dto.BookAuthor))
                book.Author = dto.BookAuthor;
            if (!string.IsNullOrEmpty(dto.BookPublisher))
                book.Publisher = dto.BookPublisher;
            if(!string.IsNullOrEmpty(dto.BookOriginalLanguage))
                book.OriginalLanguage = dto.BookOriginalLanguage;
            if (dto.BookPages.HasValue && dto.BookPages.Value > 0)
                book.Pages = dto.BookPages.Value;
            if(dto.BookGenres.Count > 0)
                book.Genres = dto.BookGenres;
        }
        public async Task<List<ContentDetailsDto>> SearchContentAsync(ContentSearch contentSearch)
        {
            var contents = await contentRepository.SearchContentAsync(contentSearch);
            if (contents == null || contents.Count <= 0)
                return null;
            var result = new List<ContentDetailsDto>();
            foreach(var content in contents)
            {
                result.Add(mapper.Map<ContentDetailsDto>(content));
            }
            return result;
        }
    }
}
