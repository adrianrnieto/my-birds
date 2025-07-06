using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Read;

internal class FamilyReadRepository(AppDbContext appDbContext) : IFamilyReadRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<Family>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Families.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetMissingFamiliesAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Families.GetMissingByNamesAsync(names, cancellationToken);
    }

    public async Task<IEnumerable<Family>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Families.AsNoTracking().Where(f => names.Contains(f.Name)).ToListAsync(cancellationToken);
    }
}
