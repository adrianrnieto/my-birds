using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Write;

internal class FamilyWriteRepository(AppDbContext appDbContext) : IFamilyWriteRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(IEnumerable<Family> families, CancellationToken cancellationToken)
    {
        await _appDbContext.Families.AddRangeAsync(families, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
