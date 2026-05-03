using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Domain.Taxonomy.Repositories;

public interface IFamilyReadRepository
{
    Task<IEnumerable<string>> GetMissingFamiliesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Family>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Family>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
}
