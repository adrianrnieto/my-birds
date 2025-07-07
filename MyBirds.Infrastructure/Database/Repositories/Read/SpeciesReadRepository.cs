using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Birds;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Read;

internal class SpeciesReadRepository(AppDbContext appDbContext) : ISpeciesReadRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<Species>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Species.AsNoTracking().Where(f => names.Contains(f.Name)).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetMissingAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Species.GetMissingByNamesAsync(names, cancellationToken);
    }
}
