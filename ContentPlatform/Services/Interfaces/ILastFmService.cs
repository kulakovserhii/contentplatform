using ContentPlatform.Dto_s;

namespace ContentPlatform.Services.Interfaces
{
    public interface ILastFmService
    {
        Task<List<MusicCreateDto>> GetPopularMusic(int count);
    }
}
