namespace MyBirds.Domain.Classifications;

public interface IGenusReadRepository
{
    Task<IEnumerable<string>> GetMissingGeneraAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Genus>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Genus>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
}
