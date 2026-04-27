using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Dto_s;
using ContentPlatform.Models;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.ExternalApi
{
    public class ContentUpdateWorker(IServiceProvider serviceProvider, ILogger<ContentUpdateWorker> logger): BackgroundService
    {
        private const int limit = 150;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    logger.LogInformation("Checking for new content at: ", DateTime.Now);
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var tmdbService = scope.ServiceProvider.GetRequiredService<ITmdbService>();
                        var contentService = scope.ServiceProvider.GetRequiredService<IContentService>();
                        var contentRepository = scope.ServiceProvider.GetRequiredService<IContentRepository>();
                        var lastFmService = scope.ServiceProvider.GetRequiredService<ILastFmService>();
                        int currentFilmCount = await contentRepository.GetCountAsync<Film>();
                        if (currentFilmCount < limit)
                        {
                            int filmsToAddCount = currentFilmCount < 75 ? (75 - currentFilmCount) : 1;
                            var candidates = await tmdbService.GetPopularFilmsAsync(filmsToAddCount);
                            int addedFilmsInThisCycle = 0;
                            foreach (var candidate in candidates)
                            {
                                if (addedFilmsInThisCycle >= filmsToAddCount)
                                    break;
                                if(!await contentRepository.ExistsByExternalId(candidate.ExternalId!))
                                {
                                    await contentService.CreateFilmAsync(candidate);
                                    addedFilmsInThisCycle++;
                                }
                            }
                        }
                        int currentMusicCount = await contentRepository.GetCountAsync<Music>();
                        if(currentMusicCount < limit)
                        {
                            int musicToAddCount = currentMusicCount < 75 ? (75 - currentMusicCount) : 1;
                            var musicCandidates = await lastFmService.GetPopularMusic(musicToAddCount);
                            int addedMusicInThisCycle = 0;
                            foreach(var musicDto in musicCandidates)
                            {
                                if (addedMusicInThisCycle >= musicToAddCount)
                                    break;
                                try
                                {
                                    if (!await contentRepository.ExistsByExternalId(musicDto.ExternalId!))
                                    {
                                        await contentService.CreateMusicAsync(musicDto);
                                        addedMusicInThisCycle++;
                                        logger.LogInformation("Added music: {Artist} - {Title}");
                                    }
                                }
                                catch(Exception ex)
                                {
                                    logger.LogError("Failed to load track {Title}: {Message}");
                                }
                            }
                        }
                        else
                        {
                            logger.LogInformation("Daily update: limit 150 filmes was reached");
                        }
                    }
                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, "Error occurred while updating content.");
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
            }
        }
    }
}
