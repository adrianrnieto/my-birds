using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBirds.Application.Services;
using MyBirds.Domain.Birds;
using MyBirds.Infrastructure.Database.Contexts;
using MyBirds.Infrastructure.Database.Repositories;
using MyBirds.Infrastructure.FileSystem;
using MyBirds.Infrastructure.HostedServices;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration["Database:ConnectionString"], x => x.MigrationsAssembly("MyBirds.Migrations"));
            //options.LogTo(Console.WriteLine);
        });
    }

    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IPhotoRepository, PhotoRepository>();
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IFileSystemScanner, FileSystemScanner>();
    }

    public static IServiceCollection ConfigureHostedServices(this IServiceCollection services)
    {
        return services
            .AddHostedService<PhotoScannerHostedService>();
    }
}
