using Microsoft.EntityFrameworkCore;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;

namespace MyBirds.Infrastructure.Database.Repositories.Read;

internal class OrderReadRepository(AppDbContext appDbContext) : IOrderReadRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<string>> GetMissingOrdersAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Orders.GetMissingByNamesAsync(names, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Orders.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken)
    {
        return await _appDbContext.Orders.AsNoTracking().Where(o => names.Contains(o.Name)).ToListAsync(cancellationToken);
    }
}
