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
                        int currentFilmCount = await contentRepository.GetCountAsync<Film>();
                        if (currentFilmCount < limit)
                        {
                            var candidates = await tmdbService.GetPopularFilmsAsync(15);
                            FilmCreateDto? filmCreateDto = null;
                            foreach (var candidate in candidates)
                            {
                                bool exists = await contentRepository.ExistsByExternalId(candidate.ExternalId!);
                                if (!exists)
                                {
                                    filmCreateDto = candidate;
                                    break;
                                }
                            }
                            if (filmCreateDto != null)
                            {
                                await contentService.CreateFilmAsync(filmCreateDto);
                                logger.LogInformation("Daily update: added film '{Title}'");
                            }
                            else
                            {
                                logger.LogWarning("Daily update: No new films today");
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
