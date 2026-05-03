using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Domain.Taxonomy.Repositories;

public interface IGenusWriteRepository
{
    Task AddAsync(IEnumerable<Genus> genera, CancellationToken cancellationToken);
}
