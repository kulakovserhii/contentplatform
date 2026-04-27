using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Models;
using AutoMapper;
using ContentPlatform.Services.Interfaces;
using System.Globalization;
namespace ContentPlatform.Services.Implementations
{
    public class ContentService(IContentRepository contentRepository, IReviewRepository reviewRepository, IMapper mapper) : IContentService
    {
        public Task<List<ContentDetailsDto>> GetAllContentByTypeAsync(ContentType contentType)
        {
            throw new NotImplementedException();
        }
        public async Task<ContentDetailsDto> GetContentByIdAsync(int contentId, int? userId)
        {
            var content = await contentRepository.GetContentWithReviewAsync(contentId);
            if (content == null)
                return null;
            var contentDto = mapper.Map<ContentDetailsDto>(content);
            if(userId.HasValue && content.Reviews != null && content.Reviews.Count > 0)
            {
                var reviewIds = contentDto.Reviews.Select(r => r.Id).ToList();
                var userVotes = await reviewRepository.GetUserVotesForReviews(userId.Value, reviewIds);
                foreach(var reviewDto in contentDto.Reviews)
                {
                    if(userVotes.TryGetValue(reviewDto.Id, out var voteType))
                    {
                        reviewDto.CurrentUserVote = voteType;
                    }
                }
            }
            return contentDto;
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
        public async Task<List<ContentSmallInfo>> GetContentsSmallInfo()
        {
            var contents = await contentRepository.GetAllContentAsync();
            if (contents == null || contents == new List<Content>())
                return null;
            var result = new List<ContentSmallInfo>();
            foreach(var content in contents)
            {
                result.Add(mapper.Map<ContentSmallInfo>(content));
            }
            return result;
        }

        public async Task<Film> CreateFilmAsync(FilmCreateDto filmCreateDto)
        {
            BaseValidation(filmCreateDto);
            if(string.IsNullOrEmpty(filmCreateDto.FilmDirector))
                throw new Exception("Film director field must be filled");
            if(filmCreateDto.FilmGenres == null || filmCreateDto.FilmGenres.Count < 1)
                throw new Exception("Film genres field must be filled with at least one genre");
            var film = new Film
            {
                Title = filmCreateDto.Title,
                Description = filmCreateDto.Description,
                ReleaseYear = filmCreateDto.ReleaseYear,
                ReleaseDate = filmCreateDto.ReleaseDate,
                ImageUrl = filmCreateDto.ImageUrl,
                Director = filmCreateDto.FilmDirector!,
                Writers = filmCreateDto.FilmWriters!,
                MainCast = filmCreateDto.FilmMainCast!,
                Budget = filmCreateDto.FilmBudget ?? 0,
                BoxOffice = filmCreateDto.FilmBoxOffice ?? 0,
                RuntimeInMinutes = filmCreateDto.FilmRuntimeInMinutes ?? 0,
                CountryOfOrigin = filmCreateDto.FilmCountryOfOrigin,
                Awards = filmCreateDto.FilmAwards,
                Genres = filmCreateDto.FilmGenres,
            };
            await contentRepository.CreateContentAsync(film);
            return film;
        }

        public async Task<TVShow> CreateTVShowAsync(TVShowCreateDto tvShowCreateDto)
        {
            BaseValidation(tvShowCreateDto);
            var tvShow = new TVShow
            {
                Title = tvShowCreateDto.Title,
                Description = tvShowCreateDto.Description,
                ReleaseYear = tvShowCreateDto.ReleaseYear,
                ReleaseDate = tvShowCreateDto.ReleaseDate,
                ImageUrl = tvShowCreateDto.ImageUrl,
                Director = tvShowCreateDto.TVShowDirector!,
                Creators = tvShowCreateDto.TVShowCreators!,
                MainCast = tvShowCreateDto.TVShowMainCast!,
                Networks = tvShowCreateDto.TVShowNetworks!,
                TotalSeasons = tvShowCreateDto.TVShowTotalSeasons ?? 0,
                TotalEpisodes = tvShowCreateDto.TVShowTotalEpisodes ?? 0,
                Genres = tvShowCreateDto.TVShowGenres ?? new(),
                EndDate = tvShowCreateDto.TVShowEndDate ?? null,
            };
            await contentRepository.CreateContentAsync(tvShow);
            return tvShow;
        }

        public async Task<Episode> CreateEpisodeAsync(EpisodeCreateDto episodeCreateDto, string? externallId = null, string? externalShowId = null)
        {
            BaseValidation(episodeCreateDto);
            int actualShowId;
            if (episodeCreateDto.EpisodeTVShowId.HasValue)
            {
                actualShowId = episodeCreateDto.EpisodeTVShowId.Value;
            }
            else if (string.IsNullOrEmpty(externalShowId))
            {
                var show = await contentRepository.GetIdByExternalId(externalShowId);
                if (show == null)
                    throw new Exception($"TV Show with externalId {externalShowId} is not found");
                actualShowId = show.Id;
            }
            else
                throw new Exception("TV Show reference is missing (both internal Id and external Id are null)");
            var episode = new Episode
            {
                Title = episodeCreateDto.Title,
                Description = episodeCreateDto.Description,
                ReleaseYear = episodeCreateDto.ReleaseYear,
                ReleaseDate = episodeCreateDto.ReleaseDate,
                ImageUrl = episodeCreateDto.ImageUrl,
                TVShowId = actualShowId,
                SeasonNumber = episodeCreateDto.EpisodeSeasonNumber ?? 0,
                EpisodeNumber = episodeCreateDto.EpisodeNumber ?? 0,
                TotalNumber = episodeCreateDto.EpisodesTotalNumber ?? 0,
                RuntimeInMinutes = episodeCreateDto.EpisodeRuntimeInMinutes ?? 0,
                ExternalId = externallId,
            };
            await contentRepository.CreateContentAsync(episode);
            return episode;
        }

        public async Task<Book> CreateBookAsync(BookCreateDto bookCreateDto)
        {
            BaseValidation(bookCreateDto);
            var book = new Book
            {
                Title = bookCreateDto.Title,
                Description = bookCreateDto.Description,
                ReleaseYear = bookCreateDto.ReleaseYear,
                ReleaseDate = bookCreateDto.ReleaseDate,
                ImageUrl = bookCreateDto.ImageUrl,
                Author = bookCreateDto.BookAuthor!,
                Publisher = bookCreateDto.BookPublisher ?? "No publisher",
                OriginalLanguage = bookCreateDto.BookOriginalLanguage,
                Pages = bookCreateDto.BookPages,
                Genres = bookCreateDto.BookGenres,
            };
            await contentRepository.CreateContentAsync(book);
            return book;
        }

        public async Task<Music> CreateMusicAsync(MusicCreateDto musicCreateDto)
        {
            BaseValidation(musicCreateDto);
            var music = new Music
            {
                Title = musicCreateDto.Title,
                Description = musicCreateDto.Description,
                ReleaseYear = musicCreateDto.ReleaseYear,
                ReleaseDate = musicCreateDto.ReleaseDate,
                ImageUrl = musicCreateDto.ImageUrl,
                Artist = musicCreateDto.MusicArtist!,
                Album = musicCreateDto.MusicAlbum ?? "Single",
                DurationInSeconds = musicCreateDto.MusicDurationInSeconds ?? 0,
                Label = musicCreateDto.MusicLabel ?? "No label",
                Lanquage = musicCreateDto.MusicLanquage,
                Genres = musicCreateDto.MusciGenres,
            };
            await contentRepository.CreateContentAsync(music);
            return music;
        }

        public async Task<Game> CreateGameDto(GameCreateDto gameCreateDto)
        {
            BaseValidation(gameCreateDto);
            var game = new Game
            {
                Title = gameCreateDto.Title,
                Description = gameCreateDto.Description,
                ReleaseYear = gameCreateDto.ReleaseYear,
                ReleaseDate = gameCreateDto.ReleaseDate,
                ImageUrl = gameCreateDto.ImageUrl,
                Developer = gameCreateDto.GameDeveloper!,
                Publisher = gameCreateDto.GamePublisher!,
                Platforms = gameCreateDto.GamePlatforms,
                Genres = gameCreateDto.GameGenres,
            };
            await contentRepository.CreateContentAsync(game);
            return game;
        }
        private void BaseValidation(ContentCreateDto contentCreateDto)
        {
            if (string.IsNullOrEmpty(contentCreateDto.Title) ||
                string.IsNullOrEmpty(contentCreateDto.Description))
                throw new Exception("Title and description are required");
        }
    }
}
