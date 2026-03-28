using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface IContentService
    {
        Task<Content> CreateContentAsync(UniversalContentDto universalContentDto);
        Task<ContentDetailsDto> GetContentByIdAsync(int contentId, int? userId);
        Task<List<ContentDetailsDto>> GetAllContentByTypeAsync(ContentType contentType);
        Task<List<ContentDetailsDto>> GetAllContentWithoutReviewsAsync();
        Task<string> DeleteContentAsync(int contentId);
        Task<ContentDetailsDto> UpdateContentAsync(int contentId, UpdateContentDto dto);
        Task<List<ContentDetailsDto>> SearchContentAsync(ContentSearch contentSearch);
        Task<List<ContentSmallInfo>> GetContentsSmallInfo();
    }
}
