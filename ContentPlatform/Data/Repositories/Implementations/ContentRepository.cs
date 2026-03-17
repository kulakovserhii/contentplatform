using AutoMapper;
using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;

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
            var content = await appDbContext.Contents.Include(c => c.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(c => c.Id == contentId);
            return content;
        }

        public async Task<T> UpdateContentAsync<T>(T content) where T : Content
        {
            appDbContext.Set<T>().Update(content);
            await appDbContext.SaveChangesAsync();
            return content;
        }
    }
}
