using AutoMapper;
using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Dto_s;
using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using ContentPlatform.Helpers;
using ContentPlatform.Enums;
using ContentPlatform.Models;

namespace ContentPlatform.Data.Repositories.Implementations
{
    public class ContentRepository(AppDbContext appDbContext, IMapper mapper) : IContentRepository
    {

        public async Task<T> CreateContentAsync<T>(T content) where T : Content
        {
            appDbContext.Set<T>().Add(content);
            await appDbContext.SaveChangesAsync();
            return content;
        }

        public async Task<bool> DeleteContent<T>(Content content) where T : Content
        {
            appDbContext.Contents.Remove(content);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Content>> GetAllContentAsync()
        {
            var contents = await appDbContext.Contents.AsNoTracking().ToListAsync();
            return contents;
        }

        public async Task<T?> GetContentByIdAsync<T>(int contentId) where T : Content
        {
            var content = await appDbContext.Set<T>().Include(c => c.Reviews).AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == contentId);
            return content;
        }

        public async Task<Content?> GetContentWithReviewAsync(int contentId)
        {
            var content = await appDbContext.Contents.Include(c => c.Reviews).ThenInclude(r => r.User)
                .Include(c => c.Reviews).ThenInclude(r => r.RateReviews)
                .FirstOrDefaultAsync(c => c.Id == contentId);
            return content;
        }

        public async Task<List<Content>> SearchContentAsync(ContentSearch searchParams)
        {
            var contentTypes = searchParams.ContentTypes ?? Enum.GetValues<ContentType>().ToList();
            var result = new List<Content>();
            foreach(var contentType in contentTypes)
            {
                switch (contentType) 
                {
                    case ContentType.Film:
                        var films = await SearchFilms(searchParams);
                        result.AddRange(films);
                        break;
                    case ContentType.TVShow:
                        var tvShows = await SearchTVShows(searchParams);
                        result.AddRange(tvShows);
                        break;
                    case ContentType.Book:
                        var books = await SearchBooks(searchParams);
                        result.AddRange(books);
                        break;
                    case ContentType.Episode:
                        var episodes = await SearchEpisodes(searchParams);
                        result.AddRange(episodes);
                        break;
                    case ContentType.Music:
                        var musices = await SearchMusices(searchParams);
                        result.AddRange(musices);
                        break;
                    case ContentType.Game:
                        var games = await SearchGames(searchParams);
                        result.AddRange(games);
                        break;
                }
            }
            return SortResults(result, searchParams);
        }
        private List<Content> SortResults(List<Content> results, ContentSearch contentSearch)
        {
            return contentSearch.SortBy switch
            {
                ContentSortBy.ReleaseYear => contentSearch.SortType == SortType.Ascending ?
                    results.OrderBy(r => r.ReleaseYear).ToList() :
                    results.OrderByDescending(r => r.ReleaseYear).ToList(),
                ContentSortBy.AverageRating => contentSearch.SortType == SortType.Ascending ?
                    results.OrderBy(r => r.AverageRating).ToList() :
                    results.OrderByDescending(r => r.AverageRating).ToList(),
                _=> results.OrderBy(r => r.AverageRating).ToList(),
            };
        }
        private IQueryable<T> ApplyBaseFilters<T>(IQueryable<T> query, ContentSearch contentSearch) where T: Content
        {
            if (contentSearch.YearFrom.HasValue)
                query = query.Where(c => c.ReleaseYear >= contentSearch.YearFrom);
            if (contentSearch.YearTo.HasValue)
                query = query.Where(c => c.ReleaseYear <= contentSearch.YearTo);
            if (contentSearch.MinRating.HasValue)
                query = query.Where(c => c.AverageRating >= contentSearch.MinRating);
            if (!string.IsNullOrEmpty(contentSearch.ContentName))
            {
                var contentName = contentSearch.ContentName.ToLower();
                query = query.Where(f => f.Title.ToLower().Contains(contentName)
                    || f.Description.ToLower().Contains(contentName));
            }
            return query;
        }
        private async Task<List<Film>> SearchFilms(ContentSearch contentSearch)
        {
            var query = appDbContext.Filmes.Include(f => f.Reviews).AsNoTracking().AsQueryable();
            query = ApplyBaseFilters(query, contentSearch);
            if(contentSearch.FilmGenres != null && contentSearch.FilmGenres.Any())
            {
                query = query.Where(f => f.Genres.Any(g => contentSearch.FilmGenres.Contains(g)));
            }
            return await query.ToListAsync();
        }
        private async Task<List<TVShow>> SearchTVShows(ContentSearch contentSearch)
        {
            var query = appDbContext.TVShows.Include(ts => ts.Reviews).AsNoTracking().AsQueryable();
            query = ApplyBaseFilters(query, contentSearch);
            if(contentSearch.TVShowGenres != null && contentSearch.TVShowGenres.Any())
            {
                query = query.Where(ts => ts.Genres.Any(g => contentSearch.TVShowGenres.Contains(g)));
            }
            return await query.ToListAsync();
        }
        private async Task<List<Music>> SearchMusices(ContentSearch contentSearch)
        {
            var query = appDbContext.Musices.Include(m => m.Reviews).AsNoTracking().AsQueryable();
            query = ApplyBaseFilters(query, contentSearch);
            if (!string.IsNullOrEmpty(contentSearch.ContentName))
            {
                query = query.Where(m => m.Artist.ToLower().Contains(contentSearch.ContentName.ToLower()) ||
                    m.Album.ToLower().Contains(contentSearch.ContentName.ToLower()));
            }
            if (contentSearch.MusicGenres != null && contentSearch.MusicGenres.Any())
            {
                query = query.Where(m => m.Genres.Any(g => contentSearch.MusicGenres.Contains(g)));
            }
            return await query.ToListAsync();
        }
        private async Task<List<Game>> SearchGames(ContentSearch contentSearch)
        {
            var query = appDbContext.Games.Include(g => g.Reviews).AsNoTracking().AsQueryable();
            query = ApplyBaseFilters(query, contentSearch);
            if (!string.IsNullOrEmpty(contentSearch.ContentName))
            {
                query = query.Where(g => g.Developer.ToLower().Contains(contentSearch.ContentName.ToLower()));
            }
            if(contentSearch.GameGenres != null && contentSearch.GameGenres.Any())
            {
                query = query.Where(g => g.Genres.Any(g => contentSearch.GameGenres.Contains(g)));
            }
            return await query.ToListAsync();
        }
        private async Task<List<Book>> SearchBooks(ContentSearch contentSearch)
        {
            var query = appDbContext.Books.Include(b => b.Reviews).AsNoTracking().AsQueryable();
            query = ApplyBaseFilters(query, contentSearch);
            if(contentSearch.BookGenres != null && contentSearch.BookGenres.Any())
            {
                query = query.Where(b => b.Genres.Any(g => contentSearch.BookGenres.Contains(g)));
            }
            return await query.ToListAsync();
        }
        private async Task<List<Episode>> SearchEpisodes(ContentSearch contentSearch)
        {
            var query = appDbContext.Episodes.Include(e => e.Reviews).Include(e => e.TVShow).AsNoTracking().AsQueryable();
            query = ApplyBaseFilters(query, contentSearch);
            if(contentSearch.TVShowGenres != null && contentSearch.TVShowGenres.Any())
            {
                var tvShowIds = await appDbContext.TVShows.Where(t => t.Genres.Any(g => contentSearch.TVShowGenres.Contains(g))).Select(t => t.Id).ToListAsync();
                query = query.Where(e => tvShowIds.Contains(e.TVShowId));
            }
            return await query.ToListAsync();
        }

        public async Task<T> UpdateContentAsync<T>(T content) where T : Content
        {
            appDbContext.Set<T>().Update(content);
            await appDbContext.SaveChangesAsync();
            return content;
        }

        public async Task UpdateContentRating(int contentId)
        {
            var content = await appDbContext.Contents.Include(c => c.Reviews).FirstOrDefaultAsync(c => c.Id == contentId);
            if (content == null) 
                return;
            if (content.Reviews.Any())
            {
                content.AverageRating = content.Reviews.Average(r => r.Rating);
                content.NumberOfRatings = content.Reviews.Count();
            }
            else
            {
                content.AverageRating = 0;
                content.NumberOfRatings = 0;
            }
            await appDbContext.SaveChangesAsync();
        }

        public async Task<TVShow>? GetIdByExternalId(string externalId)
        {
            var tvshow = await appDbContext.TVShows.FirstOrDefaultAsync(ts => ts.ExternalId == externalId);
            return tvshow;
        }
    }
}