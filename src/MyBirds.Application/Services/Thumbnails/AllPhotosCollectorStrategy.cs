using Microsoft.Extensions.Options;
using MyBirds.Application.Options;

namespace MyBirds.Application.Services.Thumbnails;

internal class AllPhotosCollectorStrategy(IOptions<PhotoStorageOptions> options)
    : IPhotoCollectorStrategy
{
    public Task<IEnumerable<string>> CollectAsync(CancellationToken cancellationToken)
    {
        var photoRootPath = Path.GetFullPath(options.Value.PhotoRootPath);
        if (!Directory.Exists(photoRootPath))
        {
            return Task.FromResult(Enumerable.Empty<string>());
        }
        var allFiles = Directory.EnumerateFiles(photoRootPath, "*", SearchOption.AllDirectories);
        return Task.FromResult(allFiles);
    }
}
