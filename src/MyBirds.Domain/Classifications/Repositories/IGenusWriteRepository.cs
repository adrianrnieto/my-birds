namespace MyBirds.Domain.Classifications.Repositories;

public interface IGenusWriteRepository
{
    Task AddAsync(IEnumerable<Genus> genera, CancellationToken cancellationToken);
}
