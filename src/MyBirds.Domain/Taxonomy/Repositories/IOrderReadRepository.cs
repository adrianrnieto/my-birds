using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Domain.Taxonomy.Repositories;

public interface IOrderReadRepository
{
    Task<IEnumerable<string>> GetMissingOrdersAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
}
