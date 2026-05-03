using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Domain.Taxonomy.Repositories;

public interface ISpeciesReadRepository
{
    Task<IEnumerable<string>> GetMissingAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Species>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Species>> GetAllAsync(CancellationToken cancellationToken);
}
