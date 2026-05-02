using MyBirds.Domain.Birds;

namespace MyBirds.Domain.Classifications;

public interface IPhotoReadRepository
{
    Task<IEnumerable<string>> GetMissingByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken);
    Task<IEnumerable<Photo>> GetFavouritesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Photo>> GetBySpeciesIdAsync(int speciesId, CancellationToken cancellationToken);
    Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Photo?> GetFavouriteBySpeciesAsync(int speciesId, CancellationToken cancellationToken);
}
