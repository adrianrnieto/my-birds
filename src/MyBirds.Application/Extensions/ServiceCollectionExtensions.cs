using MyBirds.Application.Commands.AddFavouritePhoto;
using MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;
using MyBirds.Application.Commands.ScanPhotos;
using MyBirds.Application.Queries.GetFavourites;
using MyBirds.Application.Queries.GetPhotosBySpecies;
using MyBirds.Application.Services.Locations;
using MyBirds.Application.Services.Paths;
using MyBirds.Application.Services.Thumbnails;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplicationHandlers(this IServiceCollection services)
    {
        return services
            .ConfigureQueryHandlers()
            .ConfigureCommandHandlers()
            .ConfigureApplicationServices();
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

    private static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<ILocationToCountryResolver, LocationToCountryResolver>()
            .AddSingleton<IDateToCountryResolver, DateToCountryResolver>()
            .AddSingleton<ICameraToCountryResolver, CameraToCountryResolver>()
            .AddSingleton<ICountryDetector, CountryDetector>()
            .AddSingleton<IBirdResourcePathParser, BirdResourcePathParser>()
            .AddScoped<IThumbnailBatchGenerator, ThumbnailBatchGenerator>()
            .AddScoped<IThumbnailGenerator, ThumbnailGenerator>()
            .AddScoped<IPhotoCollectorStrategy, OnlyFavouritesCollectorStrategy>()
            .AddScoped<IThumbnailPathService, ThumbnailPathService>();
    }
}
