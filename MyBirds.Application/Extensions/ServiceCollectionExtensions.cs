using MyBirds.Application.Abstract;
using MyBirds.Application.Commands.AddFavouritePhoto;
using MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;
using MyBirds.Application.Commands.ScanPhotos;
using MyBirds.Application.Queries.GetFavourites;
using MyBirds.Application.Queries.GetPhotosBySpecies;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplicationHandlers(this IServiceCollection services)
    {
        return services
            .ConfigureQueryHandlers()
            .ConfigureCommandHandlers();
    }

    private static IServiceCollection ConfigureQueryHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<IAsyncQueryHandler<GetFavouritesQueryResult>, GetFavouritesQueryHandler>()
            .AddScoped<IAsyncQueryHandler<GetPhotosBySpeciesQuery, GetPhotosBySpeciesQueryResult>, GetPhotosBySpeciesQueryHandler>();
    }

    private static IServiceCollection ConfigureCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<IAsyncCommandHandler<ScanPhotosCommand>, ScanPhotosCommandHandler>()
            .AddScoped<IAsyncCommandHandler<RegisterPhotosAndTaxonomyCommand>, RegisterPhotosAndTaxonomyCommandHandler>()
            .AddScoped<IAsyncCommandHandler<AddFavouritePhotoCommand>, AddFavouritePhotoCommandHandler>();
    }
}
