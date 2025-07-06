using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Birds;
using MyBirds.Infrastructure.Database.Contexts;
using MyBirds.Infrastructure.Shared;

namespace MyBirds.Infrastructure.Database.Repositories;

internal class PhotoRepository(AppDbContext appDbContext) : BaseRepository(appDbContext), IPhotoRepository
{
    public async Task<IEnumerable<string>> GetMissingPhotosAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Photos.GetMissingByNamesAsync(names, cancellationToken);
        /*var existingPaths = await _appDbContext.Photos
            .Where(p => names.Contains(p.Name))
            .Select(p => p.Name)
            .ToListAsync(cancellationToken);

        return names.Except(existingPaths);*/
    }

    public async Task AddAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken)
    {
        await _appDbContext.Photos.AddRangeAsync(photos, cancellationToken);
    }
}
