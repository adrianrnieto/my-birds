using MyBirds.Domain.Birds;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Write;

internal class PhotoWriteRepository(AppDbContext appDbContext)
    : IPhotoWriteRepository
{
    public async Task AddAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken)
    {
        await appDbContext.Photos.AddRangeAsync(photos, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken)
    {
        appDbContext.Photos.UpdateRange(photos);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}