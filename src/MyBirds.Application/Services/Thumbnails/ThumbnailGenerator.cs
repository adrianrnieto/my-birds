using Microsoft.Extensions.Options;
using MyBirds.Application.Options;
using MyBirds.Application.Services.Files;

namespace MyBirds.Application.Services.Thumbnails;

public interface IThumbnailGenerator
{
    Task<bool> GenerateAsync(string originalPhotoRelativePath, CancellationToken cancellationToken);
}

internal class ThumbnailGenerator(IImageEditor imageEditor, IOptions<PhotoStorageOptions> options)
    : IThumbnailGenerator
{
    private readonly string _originalPhotoRootPath = Path.GetFullPath(options.Value.PhotoRootPath);
    private readonly string _thumbnailRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, options.Value.ThumbnailOutputFolderName));
    private readonly int _thumbnailSize = options.Value.ThumbnailSize;

    public async Task<bool> GenerateAsync(string originalPhotoRelativePath, CancellationToken cancellationToken)
    {
        var originalPhotoFullPath = Path.Combine(_originalPhotoRootPath, originalPhotoRelativePath);
        var thumbnailPath = BuildSafePath(_thumbnailRootPath, originalPhotoRelativePath, ensureExists: false)
            ?? throw new ArgumentException("Unable to generate thumbnail path for {OriginalPhotoRelativePath}", nameof(originalPhotoRelativePath));

        var photoLastWriteUtc = File.GetLastWriteTimeUtc(originalPhotoFullPath);
        if (File.Exists(thumbnailPath) && File.GetLastWriteTimeUtc(thumbnailPath) >= photoLastWriteUtc)
        {
            return false;
        }

        var thumbnailDirectory = Path.GetDirectoryName(thumbnailPath);
        if (!string.IsNullOrWhiteSpace(thumbnailDirectory))
        {
            Directory.CreateDirectory(thumbnailDirectory);
        }

        await imageEditor.CopyAndResizeAsync(originalPhotoFullPath, thumbnailPath, _thumbnailSize, cancellationToken);

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
