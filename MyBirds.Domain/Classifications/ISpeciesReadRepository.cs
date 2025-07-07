using MyBirds.Domain.Birds;

namespace MyBirds.Domain.Classifications;

public interface ISpeciesReadRepository
{
    Task<IEnumerable<string>> GetMissingAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Species>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
}
