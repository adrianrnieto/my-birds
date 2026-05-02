namespace MyBirds.Domain.Classifications;

public interface IFamilyReadRepository
{
    Task<IEnumerable<string>> GetMissingFamiliesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Family>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Family>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
}
