using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface IContentService
    {
        Task<Content> CreateContentAsync(UniversalContentDto universalContentDto);
        Task<ContentDetailsDto> GetContentByIdAsync(int contentId);
        Task<List<ContentDetailsDto>> GetAllContentByTypeAsync(ContentType contentType);
        Task<List<ContentDetailsDto>> GetAllContentWithoutReviewsAsync();
        Task<string> DeleteContentAsync(int contentId);
        Task<ContentDetailsDto> UpdateContentAsync(int contentId, UpdateContentDto dto);
    }
}
