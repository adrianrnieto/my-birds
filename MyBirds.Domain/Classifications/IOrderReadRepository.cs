namespace MyBirds.Domain.Classifications;

public interface IOrderReadRepository
{
    Task<IEnumerable<string>> GetMissingOrdersAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
}
