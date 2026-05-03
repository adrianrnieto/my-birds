using MyBirds.Domain.Birds;

namespace MyBirds.Domain.Classifications.Repositories;

public interface ISpeciesWriteRepository
{
    Task AddAsync(IEnumerable<Species> species, CancellationToken cancellationToken);
}
