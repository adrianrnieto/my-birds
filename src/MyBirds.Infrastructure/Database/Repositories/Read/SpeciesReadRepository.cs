using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Taxonomy.Entities;
using MyBirds.Domain.Taxonomy.Repositories;

namespace MyBirds.Infrastructure.Database.Repositories.Read;

internal class SpeciesReadRepository(AppDbContext appDbContext) : ISpeciesReadRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<Species>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Species
            .AsNoTracking()
            .Include(s => s.Genus)
            .ThenInclude(g => g!.Family)
            .ThenInclude(f => f!.Order)
            .Where(f => names.Contains(f.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetMissingAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Species.GetMissingByNamesAsync(names, cancellationToken);
    }

    public async Task<IEnumerable<Species>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Species
            .AsNoTracking()
            .Include(s => s.Genus)
            .ThenInclude(g => g!.Family)
            .ThenInclude(f => f!.Order)
            .ToListAsync(cancellationToken);
    }
}
