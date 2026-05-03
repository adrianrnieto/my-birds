namespace MyBirds.Domain.Birds.Repositories;

public interface IPhotoRepository : IDomainRepository
{
    Task<IEnumerable<string>> GetMissingPhotosAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task AddAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken);
}
