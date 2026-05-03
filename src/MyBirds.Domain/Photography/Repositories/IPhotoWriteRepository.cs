using MyBirds.Domain.Photography.Entities;

namespace MyBirds.Domain.Photography.Repositories;

/// <summary>
/// Defines write operations for photo repository access.
/// </summary>
public interface IPhotoWriteRepository
{
    /// <summary>
    /// Adds a collection of new photos to the repository.
    /// </summary>
    /// <param name="photos">The photos to add.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    Task AddAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing collection of photos in the repository.
    /// </summary>
    /// <param name="photos">The photos to update.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    Task UpdateAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken);
}
