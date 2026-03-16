using ContentPlatform.Models;

namespace ContentPlatform.Data.Repositories.Interfaces
{
    public interface IContentRepository
    {
        Task<T?> GetContentByIdAsync<T>(int contentId) where T : Content;
        Task<List<T>> GetAllContentAsync<T>() where T : Content;
        Task<T> CreateContentAsync<T>(T content) where T : Content;
        Task<T> UpdateContentAsync<T>(T content) where T : Content;
        Task<bool> DeleteContent<T>(int contentId) where T : Content;
        Task<Content?> GetContentWithReviewAsync(int contentId);
    }
}
