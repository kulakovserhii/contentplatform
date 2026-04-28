using ContentPlatform.Dto_s;

namespace ContentPlatform.Services.Interfaces
{
    public interface IOpenLibraryService
    {
        Task<List<BookCreateDto>> GetPopularBooks(int count);
    }
}
