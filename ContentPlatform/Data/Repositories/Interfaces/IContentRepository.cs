using ContentPlatform.Dto_s;
using ContentPlatform.Models;

namespace ContentPlatform.Data.Repositories.Interfaces
{
    public interface IContentRepository
    {
        Task<T?> GetContentByIdAsync<T>(int contentId) where T : Content;
        Task<List<Content>> GetAllContentAsync();
        Task<T> CreateContentAsync<T>(T content) where T : Content;
        Task<T> UpdateContentAsync<T>(T content) where T : Content;
        Task<bool> DeleteContent<T>(Content content) where T : Content;
        Task<Content?> GetContentWithReviewAsync(int contentId);
        Task<List<Content>> SearchContentAsync(ContentSearch searchParams);
        Task UpdateContentRating(int contentId);
        Task<TVShow>? GetIdByExternalId(string externalId);
        Task<bool> ExistsByExternalId(string externalId);
        Task<int> GetCountAsync<T>() where T: Content;
        Task<TVShow?> GetLastCreatedTVShowAsync();
    }
}