namespace MyBirds.Domain.Classifications;

public interface IPhotoReadRepository
{
    Task<IEnumerable<string>> GetMissingByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
}
