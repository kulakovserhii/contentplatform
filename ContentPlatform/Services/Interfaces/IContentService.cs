using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.Models;

namespace ContentPlatform.Services.Interfaces
{
    public interface IContentService
    {
        Task<Content> CreateContentAsync(UniversalContentDto universalContentDto);
        Task<ContentDetailsDto> GetContentByIdAsync(int contentId);
        Task<List<Content>> GetAllContentByTypeAsync(ContentType contentType);
    }
}
