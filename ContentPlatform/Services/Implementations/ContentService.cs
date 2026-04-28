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
                ExternalId = filmCreateDto.ExternalId,
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
                ExternalId = bookCreateDto.ExternalId,
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
                ExternalId = musicCreateDto.ExternalId,
            };
            await contentRepository.CreateContentAsync(music);
            return music;
        }

        public async Task<Game> CreateGameAsync(GameCreateDto gameCreateDto)
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
                ExternalId = gameCreateDto.ExternalId,
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
        private void UpdateValidation(Content content, UpdateContentDto dto)
        {
            if(!string.IsNullOrEmpty(dto.Title))
                content.Title = dto.Title;
            if(!string.IsNullOrEmpty(dto.Description))
                content.Description = dto.Description;
            if(dto.ReleaseYear != null && dto.ReleaseYear.HasValue && dto.ReleaseYear.Value > 0)
                content.ReleaseYear = dto.ReleaseYear.Value;
            if(dto.ReleaseDate != null && dto.ReleaseDate.HasValue)
                content.ReleaseDate = dto.ReleaseDate.Value;
            if(!string.IsNullOrEmpty(dto.ImageUrl))
                content.ImageUrl = dto.ImageUrl;
        }
        public async Task<ContentDetailsDto> UpdateFilmAsync(int filmId, UpdateFilmDto updateFilmDto)
        {
            var film = await contentRepository.GetContentByIdAsync<Film>(filmId);
            if (film == null)
                return null;
            UpdateValidation(film, updateFilmDto);
            if (!string.IsNullOrEmpty(updateFilmDto.FilmDirector))
                film.Director = updateFilmDto.FilmDirector;
            if (!string.IsNullOrEmpty(updateFilmDto.FilmWriters))
                film.Writers = updateFilmDto.FilmWriters;
            if (!string.IsNullOrEmpty(updateFilmDto.FilmMainCast))
                film.MainCast = updateFilmDto.FilmMainCast;
            if (updateFilmDto.FilmBudget.HasValue && updateFilmDto.FilmBudget.Value > 0)
                film.Budget = updateFilmDto.FilmBudget.Value;
            if (updateFilmDto.FilmBoxOffice.HasValue && updateFilmDto.FilmBoxOffice.Value > 0)
                film.BoxOffice = updateFilmDto.FilmBoxOffice.Value;
            if (updateFilmDto.FilmRuntimeInMinutes.HasValue && updateFilmDto.FilmRuntimeInMinutes.Value > 0)
                film.RuntimeInMinutes = updateFilmDto.FilmRuntimeInMinutes.Value;
            if (!string.IsNullOrEmpty(updateFilmDto.FilmCountryOfOrigin))
                film.CountryOfOrigin = updateFilmDto.FilmCountryOfOrigin;
            if (!string.IsNullOrEmpty(updateFilmDto.FilmAwards))
                film.Awards = updateFilmDto.FilmAwards;
            if (updateFilmDto.FilmGenres.Count > 0)
                film.Genres = updateFilmDto.FilmGenres;
            await contentRepository.UpdateContentAsync(film);
            return mapper.Map<ContentDetailsDto>(film);
        }

        public async Task<ContentDetailsDto> UpdateTVShowAsync(int tVShowId, UpdateTVShowDto updateTVShowDto)
        {
            var tvShow = await contentRepository.GetContentByIdAsync<TVShow>(tVShowId);
            if(tvShow == null)
                return null;
            UpdateValidation(tvShow, updateTVShowDto);
            if (!string.IsNullOrEmpty(updateTVShowDto.TVShowDirector))
                tvShow.Director = updateTVShowDto.TVShowDirector;
            if (!string.IsNullOrEmpty(updateTVShowDto.TVShowCreators))
                tvShow.Creators = updateTVShowDto.TVShowCreators;
            if (!string.IsNullOrEmpty(updateTVShowDto.TVShowMainCast))
                tvShow.MainCast = updateTVShowDto.TVShowMainCast;
            if (updateTVShowDto.TVShowTotalSeasons.HasValue && updateTVShowDto.TVShowTotalSeasons > 0)
                tvShow.TotalSeasons = updateTVShowDto.TVShowTotalSeasons.Value;
            if (updateTVShowDto.TVShowTotalEpisodes.HasValue && updateTVShowDto.TVShowTotalEpisodes > 0)
                tvShow.TotalEpisodes = updateTVShowDto.TVShowTotalEpisodes.Value;
            if (!string.IsNullOrEmpty(updateTVShowDto.TVShowNetworks))
                tvShow.Networks = updateTVShowDto.TVShowNetworks;
            if (updateTVShowDto.TVShowEndDate.HasValue)
                tvShow.EndDate = updateTVShowDto.TVShowEndDate.Value;
            if(updateTVShowDto.TVShowGenres.Count > 0)
                tvShow.Genres = updateTVShowDto.TVShowGenres;
            await contentRepository.UpdateContentAsync(tvShow);
            return mapper.Map<ContentDetailsDto>(tvShow);
        }

        public async Task<ContentDetailsDto> UpdateEpisodeAsync(int episodeId, UpdateEpisodeDto updateEpisodeDto)
        {
            var episode = await contentRepository.GetContentByIdAsync<Episode>(episodeId);
            if (episode == null)
                return null;
            UpdateValidation(episode, updateEpisodeDto);
            if (updateEpisodeDto.EpisodeSeasonNumber.HasValue && updateEpisodeDto.EpisodeSeasonNumber > 0)
                episode.SeasonNumber = updateEpisodeDto.EpisodeSeasonNumber.Value;
            if (updateEpisodeDto.EpisodeTVShowId.HasValue && updateEpisodeDto.EpisodeTVShowId.Value != episode.TVShowId)
            {
                var tvshowExists = await contentRepository.GetContentByIdAsync<TVShow>(updateEpisodeDto.EpisodeTVShowId.Value);
                if(tvshowExists == null)
                    throw new Exception($"TV Show with Id {updateEpisodeDto.EpisodeTVShowId.Value} does not exist");
                episode.TVShowId = updateEpisodeDto.EpisodeTVShowId.Value;
            }
            if (updateEpisodeDto.EpisodeNumber.HasValue && updateEpisodeDto.EpisodeNumber > 0)
                episode.EpisodeNumber = updateEpisodeDto.EpisodeNumber.Value;
            if (updateEpisodeDto.EpisodesTotalNumber.HasValue && updateEpisodeDto.EpisodesTotalNumber > 0)
                episode.TotalNumber = updateEpisodeDto.EpisodesTotalNumber.Value;
            if (updateEpisodeDto.EpisodeRuntimeInMinutes.HasValue && updateEpisodeDto.EpisodeRuntimeInMinutes > 0)
                episode.RuntimeInMinutes = updateEpisodeDto.EpisodeRuntimeInMinutes.Value;
            await contentRepository.UpdateContentAsync(episode);
            return mapper.Map<ContentDetailsDto>(episode);
        }

        public async Task<ContentDetailsDto> UpdateBookAsync(int bookId, UpdateBookDto updateBookDto)
        {
            var book = await contentRepository.GetContentByIdAsync<Book>(bookId);
            if(book == null)
                return null;
            UpdateValidation(book, updateBookDto);
            if (!string.IsNullOrEmpty(updateBookDto.BookAuthor))
                book.Author = updateBookDto.BookAuthor;
            if (!string.IsNullOrEmpty(updateBookDto.BookPublisher))
                book.Publisher = updateBookDto.BookPublisher;
            if (!string.IsNullOrEmpty(updateBookDto.BookOriginalLanguage))
                book.OriginalLanguage = updateBookDto.BookOriginalLanguage;
            if(updateBookDto.BookPages.HasValue && updateBookDto.BookPages.Value > 0)
                book.Pages = updateBookDto.BookPages.Value;
            if(updateBookDto.BookPages.HasValue && updateBookDto.BookGenres.Count > 0 )
                book.Genres = updateBookDto.BookGenres;
            await contentRepository.UpdateContentAsync(book);
            return mapper.Map<ContentDetailsDto>(book);
        }

        public async Task<ContentDetailsDto> UpdateMusicAsync(int musicId, UpdateMusicDto updateMusicDto)
        {
            var music = await contentRepository.GetContentByIdAsync<Music>(musicId);
            if (music == null)
                return null;
            UpdateValidation(music, updateMusicDto);
            if (!string.IsNullOrEmpty(updateMusicDto.MusicArtist))
                music.Artist = updateMusicDto.MusicArtist;
            if (!string.IsNullOrEmpty(updateMusicDto.MusicAlbum))
                music.Album = updateMusicDto.MusicAlbum;
            if (updateMusicDto.MusicDurationInSeconds.HasValue && updateMusicDto.MusicDurationInSeconds.Value > 0)
                music.DurationInSeconds = updateMusicDto.MusicDurationInSeconds.Value;
            if (!string.IsNullOrEmpty(updateMusicDto.MusicLabel))
                music.Label = updateMusicDto.MusicLabel;
            if (!string.IsNullOrEmpty(updateMusicDto.MusicLanguage))
                music.Lanquage = updateMusicDto.MusicLanguage;
            if (updateMusicDto.MusicGenres.Count > 0)
                music.Genres = updateMusicDto.MusicGenres;
            await contentRepository.UpdateContentAsync(music);
            return mapper.Map<ContentDetailsDto>(music);
        }

        public async Task<ContentDetailsDto> UpdateGameAsync(int gameId, UpdateGameDto updateGameDto)
        {
            var game = await contentRepository.GetContentByIdAsync<Game>(gameId);
            if (game == null)
                return null;
            UpdateValidation(game, updateGameDto);
            if (!string.IsNullOrEmpty(updateGameDto.GamePublisher))
                game.Publisher = updateGameDto.GamePublisher;
            if (!string.IsNullOrEmpty(updateGameDto.GameDeveloper))
                game.Developer = updateGameDto.GameDeveloper;
            if (updateGameDto.GamePlatforms.Count > 0)
                game.Platforms.AddRange(updateGameDto.GamePlatforms);
            if (updateGameDto.GameGenres.Count > 0)
                game.Genres = updateGameDto.GameGenres;
            await contentRepository.UpdateContentAsync(game);
            return mapper.Map<ContentDetailsDto>(game);
        }
    }
}