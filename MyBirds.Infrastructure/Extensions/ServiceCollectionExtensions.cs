using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBirds.Application.Services;
using MyBirds.Domain.Birds;
using MyBirds.Domain.Classifications;
using MyBirds.Infrastructure.Database.Contexts;
using MyBirds.Infrastructure.Database.Repositories;
using MyBirds.Infrastructure.Database.Repositories.Read;
using MyBirds.Infrastructure.Database.Repositories.Write;
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
            .AddScoped<IPhotoRepository, PhotoRepository>()
            .ConfigureReadRepositories()
            .ConfigureWriteRepositories();
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

    public static IServiceCollection ConfigureReadRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IOrderReadRepository, OrderReadRepository>()
            .AddScoped<IFamilyReadRepository, FamilyReadRepository>()
            .AddScoped<IGenusReadRepository, GenusReadRepository>()
            .AddScoped<IPhotoReadRepository, PhotoReadRepository>();
    }

    public static IServiceCollection ConfigureWriteRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IOrderWriteRepository, OrderWriteRepository>()
            .AddScoped<IFamilyWriteRepository, FamilyWriteRepository>()
            .AddScoped<IGenusWriteRepository, GenusWriteRepository>();
    }
}
