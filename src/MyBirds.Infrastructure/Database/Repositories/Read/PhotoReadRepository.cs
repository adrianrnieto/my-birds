using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Birds;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Read;

internal class PhotoReadRepository(AppDbContext appDbContext) : IPhotoReadRepository
{
    public async Task<IEnumerable<string>> GetMissingByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await appDbContext.Photos.GetMissingByNamesAsync(names, cancellationToken);
    }

    public async Task<IEnumerable<Photo>> GetFavouritesAsync(CancellationToken cancellationToken)
    {
        return await appDbContext.Photos
            .Where(p => p.IsFavourite)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Photo>> GetBySpeciesIdAsync(int speciesId, CancellationToken cancellationToken)
    {
        return await appDbContext.Photos
            .Where(p => p.SpeciesId == speciesId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await appDbContext.Photos
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Photo?> GetFavouriteBySpeciesAsync(int speciesId, CancellationToken cancellationToken)
    {
        return await appDbContext.Photos
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.SpeciesId == speciesId && p.IsFavourite, cancellationToken);
    }
}
