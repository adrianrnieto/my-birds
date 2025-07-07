using MyBirds.Domain.Birds;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Write;

internal class PhotoWriteRepository(AppDbContext appDbContext)
    : IPhotoWriteRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken)
    {
        await _appDbContext.Photos.AddRangeAsync(photos, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}