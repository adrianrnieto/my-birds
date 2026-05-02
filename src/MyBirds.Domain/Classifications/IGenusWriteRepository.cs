namespace MyBirds.Domain.Classifications;

public interface IGenusWriteRepository
{
    Task AddAsync(IEnumerable<Genus> genera, CancellationToken cancellationToken);
}
