using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Domain.Taxonomy.Repositories;

public interface IFamilyWriteRepository
{
    Task AddAsync(IEnumerable<Family> families, CancellationToken cancellationToken);
}
