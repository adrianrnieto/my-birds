using MyBirds.Domain.Classifications.Repositories;

namespace MyBirds.Infrastructure.Database.Repositories.Write;

internal class SpeciesWriteRepository(AppDbContext appDbContext)
    : ISpeciesWriteRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(IEnumerable<Species> species, CancellationToken cancellationToken)
    {
        await _appDbContext.Species.AddRangeAsync(species, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
