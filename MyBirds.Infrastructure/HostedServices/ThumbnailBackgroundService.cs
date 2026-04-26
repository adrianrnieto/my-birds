using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBirds.Application.Options;
using MyBirds.Application.Services.Thumbnails;

namespace MyBirds.Infrastructure.HostedServices;

public class ThumbnailBackgroundService(
    ILogger<ThumbnailBackgroundService> logger,
    IServiceProvider serviceProvider,
    IOptions<PhotoStorageOptions> options)
    : BackgroundService
{
    private readonly PhotoStorageOptions _options = options.Value;
    private readonly ILogger<ThumbnailBackgroundService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var startedAt = DateTimeOffset.UtcNow;
            _logger.LogInformation("Thumbnail job started at {StartTime}", startedAt);

            ThumbnailBatchResult batchResult;
            try
            {
                using var scope = serviceProvider.CreateScope();
                var thumbnailBatchGenerator = scope.ServiceProvider.GetRequiredService<IThumbnailBatchGenerator>();
                batchResult = await thumbnailBatchGenerator.GenerateAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            var elapsed = DateTimeOffset.UtcNow - startedAt;
            _logger.LogInformation(
                "Thumbnail job finished in {DurationMs}ms. Scanned={Scanned}, Generated={Generated}, Skipped={Skipped}, Unsupported={Unsupported}, Failed={Failed}. Next run in {IntervalSeconds}s.",
                elapsed.TotalMilliseconds,
                batchResult.Scanned,
                batchResult.Generated,
                batchResult.Skipped,
                batchResult.Unsupported,
                batchResult.Failed,
                _options.ThumbnailGenerationIntervalSeconds);

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(_options.ThumbnailGenerationIntervalSeconds), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }
}
