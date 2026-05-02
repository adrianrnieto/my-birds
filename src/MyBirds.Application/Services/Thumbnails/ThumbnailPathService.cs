using Microsoft.Extensions.Options;
using MyBirds.Application.Options;

namespace MyBirds.Application.Services.Thumbnails;

public interface IThumbnailPathService
{
    string? GetThumbnailRelativePath(string? relativePhotoPath);
}

internal class ThumbnailPathService(IOptions<PhotoStorageOptions> options)
    : IThumbnailPathService
{
    private readonly string _photoRootPath = Path.GetFullPath(options.Value.PhotoRootPath);
    private readonly string _thumbnailRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, options.Value.ThumbnailOutputFolderName));

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
