using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Domain.Taxonomy.Repositories;

public interface ISpeciesWriteRepository
{
    Task AddAsync(IEnumerable<Species> species, CancellationToken cancellationToken);
}
