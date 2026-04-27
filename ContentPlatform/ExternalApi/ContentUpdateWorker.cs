using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Models;
using ContentPlatform.Services.Interfaces;

namespace ContentPlatform.ExternalApi
{
    public class ContentUpdateWorker(IServiceProvider serviceProvider, ILogger<ContentUpdateWorker> logger): BackgroundService
    {
        private const int limit = 12;

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
                        int currentFilmCount = await contentRepository.GetCountAsync<Film>();
                        if(currentFilmCount < limit)
                        {
                            int needed = limit - currentFilmCount;
                            logger.LogInformation("Need films: ", needed);
                            var popularFilms = await tmdbService.GetPopularFilmsAsync(needed);
                            foreach(var filmDto in popularFilms)
                            {
                                try
                                {
                                    bool exists = await contentRepository.ExistsByExternalId(filmDto.ExternalId!);
                                    if (!exists)
                                    {
                                        await contentService.CreateFilmAsync(filmDto);
                                        logger.LogInformation("Added film: " + filmDto.Title);
                                    }
                                }
                                catch(Exception ex)
                                {
                                    logger.LogError(ex, "Error adding film: " + filmDto.Title);
                                }
                            }
                        }
                        else
                        {
                            logger.LogInformation("No new films needed. Current count: ", currentFilmCount);
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
