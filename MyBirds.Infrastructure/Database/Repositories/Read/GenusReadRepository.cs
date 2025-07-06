using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Read;

internal class GenusReadRepository(AppDbContext appDbContext) : IGenusReadRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<Genus>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Genera.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetMissingGeneraAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Genera.GetMissingByNamesAsync(names, cancellationToken);
    }
}
