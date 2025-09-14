using MyBirds.Domain.Birds;

namespace MyBirds.Domain.Classifications;

public interface IPhotoWriteRepository
{
    Task AddAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken);
    Task UpdateAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken);
}
