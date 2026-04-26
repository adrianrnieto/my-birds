using Microsoft.Extensions.Options;
using MyBirds.Application.Abstract;
using MyBirds.Application.Queries.GetFavourites;
using MyBirds.Server.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace MyBirds.Server.Services;

public interface IThumbnailService
{
    string? GetThumbnailRelativePath(string? relativePhotoPath);
    Task<ThumbnailBatchResult> GenerateThumbnailsAsync(CancellationToken cancellationToken);
    string ThumbnailRootPath { get; }
}

public record ThumbnailBatchResult(int Scanned, int Generated, int Unsupported, int Failed);

public class ThumbnailService(
    IServiceScopeFactory scopeFactory,
    IOptions<PhotoStorageOptions> options,
    ILogger<ThumbnailService> logger)
    : IThumbnailService
{
    private readonly string _photoRootPath = Path.GetFullPath(options.Value.PhotoRootPath);
    private readonly string _thumbnailRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, options.Value.ThumbnailOutputFolderName));
    private readonly int _thumbnailSize = options.Value.ThumbnailSize;
    private readonly int _maxFilesPerRun = options.Value.MaxFilesPerRun;
    private readonly ILogger<ThumbnailService> _logger = logger;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    public string ThumbnailRootPath => _thumbnailRootPath;

    public string? GetThumbnailRelativePath(string? relativePhotoPath)
    {
        if (string.IsNullOrWhiteSpace(relativePhotoPath))
            return null;

        var normalizedRelativePath = relativePhotoPath.Replace('/', Path.DirectorySeparatorChar);
        var photoPath = BuildSafePath(_photoRootPath, normalizedRelativePath, ensureExists: true);
        if (photoPath is null || !File.Exists(photoPath))
            return null;

        var thumbnailPath = BuildSafePath(_thumbnailRootPath, normalizedRelativePath, ensureExists: false);
        if (thumbnailPath is null)
            return null;

        var photoLastWriteUtc = File.GetLastWriteTimeUtc(photoPath);
        if (File.Exists(thumbnailPath) && File.GetLastWriteTimeUtc(thumbnailPath) >= photoLastWriteUtc)
            return relativePhotoPath;

        return null;
    }

    public async Task<ThumbnailBatchResult> GenerateThumbnailsAsync(CancellationToken cancellationToken)
    {
        if (!Directory.Exists(_photoRootPath))
            return new ThumbnailBatchResult(0, 0, 0, 0);

        Directory.CreateDirectory(_thumbnailRootPath);

        var scanned = 0;
        var generated = 0;
        var unsupported = 0;
        var failed = 0;

        using (var scope = _scopeFactory.CreateScope())
        {
            var getFavouritesQueryHandler = scope.ServiceProvider.GetRequiredService<IAsyncQueryHandler<GetFavouritesQueryResult>>();

            var favourites = await getFavouritesQueryHandler.HandleAsync(cancellationToken);
            if (favourites.IsFailure)
            {
                _logger.LogError("There is an error while loading favourites: {ErrorMessage}", favourites.Error);
                return new ThumbnailBatchResult(0, 0, 0, 0);
            }

            foreach (var fav in favourites.Value!.SpeciesAndPhotosData.Select(fav => fav.Photo))
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (_maxFilesPerRun > 0 && scanned >= _maxFilesPerRun)
                    return new ThumbnailBatchResult(scanned, generated, unsupported, failed);

                scanned++;

                if (fav is null)
                {
                    failed++;
                    continue;
                }

                try
                {
                    _logger.LogInformation("Generating thumbnail for {PhotoFullPath}", fav.FullPath);
                    var success = await GenerateThumbnailAsync(fav.FullPath, cancellationToken);
                    if (success)
                    {
                        generated++;
                    }
                    else
                    {
                        _logger.LogInformation("Thumbnail for {PhotoFullPath} is up to date, skipping generation", fav.FullPath);
                    }
                }
                catch (UnknownImageFormatException ex)
                {
                    unsupported++;
                    _logger.LogDebug(ex, "Skipping unsupported file {PhotoFullPath}", fav.FullPath);
                }
                catch (NotSupportedException ex)
                {
                    unsupported++;
                    _logger.LogDebug(ex, "Skipping unsupported file {PhotoFullPath}", fav.FullPath);
                }
                catch (Exception ex)
                {
                    failed++;
                    _logger.LogWarning(ex, "Failed generating thumbnail for {PhotoFullPath}", fav.FullPath);
                }
            }

            // TODO: Move these 2 codes to an strategy pattern to support different thumbnail generation strategies (e.g. generate for all photos, generate for favourites only, etc.)
            /*
            // This code generates one thumbnail per photo file in the photo root directory,
            // but it can be very slow if there are many photos and only a few of them are favourites.
            foreach (var sourcePath in Directory.EnumerateFiles(_photoRootPath, "*", SearchOption.AllDirectories))
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (_maxFilesPerRun > 0 && scanned >= _maxFilesPerRun)
                    break;

                scanned++;

                var relativePath = Path.GetRelativePath(_photoRootPath, sourcePath);
                var thumbnailPath = BuildSafePath(_thumbnailRootPath, relativePath, ensureExists: false);
                if (thumbnailPath is null)
                {
                    failed++;
                    continue;
                }

                var photoLastWriteUtc = File.GetLastWriteTimeUtc(sourcePath);
                if (File.Exists(thumbnailPath) && File.GetLastWriteTimeUtc(thumbnailPath) >= photoLastWriteUtc)
                    continue;

                var thumbnailDirectory = Path.GetDirectoryName(thumbnailPath);
                if (!string.IsNullOrWhiteSpace(thumbnailDirectory))
                    Directory.CreateDirectory(thumbnailDirectory);

                try
                {
                    using var image = await Image.LoadAsync(sourcePath, cancellationToken);
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(_thumbnailSize, _thumbnailSize),
                        Mode = ResizeMode.Crop,
                        Position = AnchorPositionMode.Center
                    }));
                    await image.SaveAsync(thumbnailPath, cancellationToken);
                    generated++;
                }
                catch (UnknownImageFormatException ex)
                {
                    unsupported++;
                    _logger.LogDebug(ex, "Skipping unsupported file {SourcePath}", sourcePath);
                }
                catch (NotSupportedException ex)
                {
                    unsupported++;
                    _logger.LogDebug(ex, "Skipping unsupported file {SourcePath}", sourcePath);
                }
                catch (Exception ex)
                {
                    failed++;
                    _logger.LogWarning(ex, "Failed generating thumbnail for {SourcePath}", sourcePath);
                }
            }*/
        }

        return new ThumbnailBatchResult(scanned, generated, unsupported, failed);
    }

    private async Task<bool> GenerateThumbnailAsync(string originalPhotoRelativePath, CancellationToken cancellationToken)
    {
        var originalPhotoFullPath = Path.Combine(_photoRootPath, originalPhotoRelativePath);
        var thumbnailPath = BuildSafePath(_thumbnailRootPath, originalPhotoRelativePath, ensureExists: false);
        if (thumbnailPath is null)
        {
            return false;
        }

        var photoLastWriteUtc = File.GetLastWriteTimeUtc(originalPhotoFullPath);
        if (File.Exists(thumbnailPath) && File.GetLastWriteTimeUtc(thumbnailPath) >= photoLastWriteUtc)
        {
            return false;
        }

        var thumbnailDirectory = Path.GetDirectoryName(thumbnailPath);
        if (!string.IsNullOrWhiteSpace(thumbnailDirectory))
            Directory.CreateDirectory(thumbnailDirectory);

        using var image = await Image.LoadAsync(originalPhotoFullPath, cancellationToken);
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(_thumbnailSize, _thumbnailSize),
            Mode = ResizeMode.Crop,
            Position = AnchorPositionMode.Center
        }));
        await image.SaveAsync(thumbnailPath, cancellationToken);

        return true;
    }

    private static string? BuildSafePath(string rootPath, string relativePath, bool ensureExists)
    {
        var combinedPath = Path.GetFullPath(Path.Combine(rootPath, relativePath));
        if (!combinedPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
            return null;

        if (ensureExists && !File.Exists(combinedPath))
            return null;

        return combinedPath;
    }
}
