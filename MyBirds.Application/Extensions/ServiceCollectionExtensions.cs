using MyBirds.Application.Abstract;
using MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;
using MyBirds.Application.Commands.ScanPhotos;

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
        return services;
    }

    private static IServiceCollection ConfigureCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<IAsyncCommandHandler<ScanPhotosCommand>, ScanPhotosCommandHandler>()
            .AddScoped<IAsyncCommandHandler<RegisterPhotosAndTaxonomyCommand>, RegisterPhotosAndTaxonomyCommandHandler>();
    }
}
