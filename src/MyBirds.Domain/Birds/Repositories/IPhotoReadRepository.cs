namespace MyBirds.Domain.Birds.Repositories;

/// <summary>
/// Defines read operations for photo repository access.
/// </summary>
public interface IPhotoReadRepository
{
    /// <summary>
    /// Retrieves photo names that are not present in the repository.
    /// </summary>
    /// <param name="names">The collection of photo names to check.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>A collection of photo names that do not exist in the repository.</returns>
    Task<IEnumerable<string>> GetMissingByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all photos marked as favourite.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>A collection of favourite photos.</returns>
    Task<IEnumerable<Photo>> GetFavouritesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all photos for a specific species.
    /// </summary>
    /// <param name="speciesId">The species identifier.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>A collection of photos for the specified species.</returns>
    Task<IEnumerable<Photo>> GetBySpeciesIdAsync(int speciesId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific photo by its identifier.
    /// </summary>
    /// <param name="id">The photo identifier.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The photo if found; otherwise null.</returns>
    Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the favourite photo for a specific species.
    /// </summary>
    /// <param name="speciesId">The species identifier.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The favourite photo for the species if found; otherwise null.</returns>
    Task<Photo?> GetFavouriteBySpeciesAsync(int speciesId, CancellationToken cancellationToken);
}
