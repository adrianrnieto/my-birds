using MyBirds.Domain.Shared;

namespace MyBirds.Domain.Birds;

// TODO: Split into ReadRepository and WriteRepository
public interface IPhotoRepository : IDomainRepository
{
    Task<IEnumerable<string>> GetMissingPhotosAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task AddAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken);
}
