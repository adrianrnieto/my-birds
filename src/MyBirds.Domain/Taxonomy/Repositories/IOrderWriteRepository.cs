using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Domain.Taxonomy.Repositories;

public interface IOrderWriteRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task AddAsync(IEnumerable<Order> orders, CancellationToken cancellationToken);
}
