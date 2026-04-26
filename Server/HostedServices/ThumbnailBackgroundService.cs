using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyBirds.Server.Options;
using MyBirds.Server.Services;

namespace MyBirds.Server.HostedServices;

public class ThumbnailBackgroundService(
    IThumbnailService thumbnailService,
    IOptions<PhotoStorageOptions> options,
    ILogger<ThumbnailBackgroundService> logger)
    : BackgroundService
{
    private readonly IThumbnailService _thumbnailService = thumbnailService;
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
                batchResult = await _thumbnailService.GenerateThumbnailsAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            var elapsed = DateTimeOffset.UtcNow - startedAt;
            _logger.LogInformation(
                "Thumbnail job finished in {DurationMs}ms. Scanned={Scanned}, Generated={Generated}, Unsupported={Unsupported}, Failed={Failed}. Next run in {IntervalSeconds}s.",
                elapsed.TotalMilliseconds,
                batchResult.Scanned,
                batchResult.Generated,
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
