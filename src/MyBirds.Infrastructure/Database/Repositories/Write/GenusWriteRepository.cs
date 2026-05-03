using MyBirds.Domain.Taxonomy.Entities;
using MyBirds.Domain.Taxonomy.Repositories;

namespace MyBirds.Infrastructure.Database.Repositories.Write;

internal class GenusWriteRepository(AppDbContext appDbContext) : IGenusWriteRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(IEnumerable<Genus> genera, CancellationToken cancellationToken)
    {
        await _appDbContext.Genera.AddRangeAsync(genera, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
