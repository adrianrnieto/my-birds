namespace MyBirds.Domain.Classifications.Repositories;

public interface IOrderWriteRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task AddAsync(IEnumerable<Order> orders, CancellationToken cancellationToken);
}
