using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyBirds.Application.Options;

namespace MyBirds.Application.Services.Thumbnails;

public interface IThumbnailBatchGenerator
{
    Task<ThumbnailBatchResult> GenerateAsync(CancellationToken cancellationToken);
}

internal class ThumbnailBatchGenerator(
    ILogger<ThumbnailBatchGenerator> logger,
    IThumbnailGenerator thumbnailGenerator,
    IPhotoCollectorStrategy photoCollectorStrategy,
    IOptions<PhotoStorageOptions> options)
    : IThumbnailBatchGenerator
{
    private readonly string _photoRootPath = Path.GetFullPath(options.Value.PhotoRootPath);
    private readonly string _thumbnailRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, options.Value.ThumbnailOutputFolderName));
    private readonly int _maxFilesPerRun = options.Value.MaxFilesPerRun;

    public async Task<ThumbnailBatchResult> GenerateAsync(CancellationToken cancellationToken)
    {
        if (!Directory.Exists(_photoRootPath))
        {
            return new ThumbnailBatchResult(0, 0, 0, 0, 0);
        }

        Directory.CreateDirectory(_thumbnailRootPath);

        var scanned = 0;
        var generated = 0;
        var skipped = 0;
        var unsupported = 0;
        var failed = 0;

        var photoPaths = await photoCollectorStrategy.CollectAsync(cancellationToken);
        foreach (var path in photoPaths)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (_maxFilesPerRun > 0 && scanned >= _maxFilesPerRun)
                break;

            scanned++;

            try
            {
                var success = await thumbnailGenerator.GenerateAsync(path, cancellationToken);
                if (success)
                {
                    generated++;
                }
                else
                {
                    skipped++;
                    logger.LogInformation("Skipping file since thumbnail already exists for {PhotoPath}", path);
                }
            }
            catch (NotSupportedException ex)
            {
                unsupported++;
                logger.LogWarning(ex, "Skipping unsupported file {PhotoPath}", path);
            }
            catch (Exception ex)
            {
                failed++;
                logger.LogError(ex, "Failed generating thumbnail for {PhotoPath}", path);
            }
        }

        return new ThumbnailBatchResult(scanned, generated, skipped, unsupported, failed);
    }
}    
