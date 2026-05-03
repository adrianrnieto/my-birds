using Microsoft.Extensions.Logging;
using MyBirds.Application.Queries.GetFavourites;

namespace MyBirds.Application.Services.Thumbnails;

internal class OnlyFavouritesCollectorStrategy(
    ILogger<OnlyFavouritesCollectorStrategy> logger,
    IAsyncQueryHandler<GetFavouritesQueryResult> getFavouritesQueryHandler)
    : IPhotoCollectorStrategy
{
    public async Task<IEnumerable<string>> CollectAsync(CancellationToken cancellationToken)
    {
        var favourites = await getFavouritesQueryHandler.HandleAsync(cancellationToken);
        if (favourites.IsFailure)
        {
            logger.LogError("There was an error while loading favourites: {ErrorMessage}", favourites.Error);
            throw new InvalidOperationException($"Error loading favourites: {favourites.Error}");
        }

        return favourites.Value!.SpeciesAndPhotosData
            .Select(fav => fav.Photo)
            .Where(photo => photo is not null)
            .Select(photo => photo!.FullPath);
    }
}
