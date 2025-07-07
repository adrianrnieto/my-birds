using MyBirds.Domain.Birds;

namespace MyBirds.Domain.Classifications;

public interface ISpeciesWriteRepository
{
    Task AddAsync(IEnumerable<Species> species, CancellationToken cancellationToken);
}
