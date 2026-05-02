using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;
using MyBirds.Infrastructure.Shared;

namespace MyBirds.Infrastructure.Database.Repositories.Write;

internal class OrderWriteRepository(AppDbContext appDbContext) : BaseRepository(appDbContext), IOrderWriteRepository
{
    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await _appDbContext.AddAsync(order, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(IEnumerable<Order> orders, CancellationToken cancellationToken)
    {
        await _appDbContext.Orders.AddRangeAsync(orders, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
