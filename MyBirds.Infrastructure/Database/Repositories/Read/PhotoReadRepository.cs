using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Read;

internal class PhotoReadRepository(AppDbContext appDbContext) : IPhotoReadRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<string>> GetMissingByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Photos.GetMissingByNamesAsync(names, cancellationToken);
    }
}
