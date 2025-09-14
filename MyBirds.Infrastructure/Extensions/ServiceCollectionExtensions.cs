using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBirds.Application.Services;
using MyBirds.Domain.Birds;
using MyBirds.Domain.Classifications;
using MyBirds.Domain.Shared;
using MyBirds.Infrastructure.Database.Contexts;
using MyBirds.Infrastructure.Database.Repositories;
using MyBirds.Infrastructure.Database.Repositories.Read;
using MyBirds.Infrastructure.Database.Repositories.Write;
using MyBirds.Infrastructure.Events.Publishers;
using MyBirds.Infrastructure.FileSystem;
using MyBirds.Infrastructure.HostedServices;
using MyBirds.Infrastructure.Messaging;

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

    public static IServiceCollection ConfigureMessageBrokers(this IServiceCollection services)
    {
        services.AddSingleton<IProducer<string, string>>(sp =>
        {
            // TODO: Use configuration to set up Kafka producer options
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            return new ProducerBuilder<string, string>(config).Build();
        });

        // TODO: Use configuration to set up Kafka producer options
        services.AddSingleton<IEventPublisher<PhotoCreatedEvent>>(sp =>
        {
            var producer = sp.GetRequiredService<IProducer<string, string>>();
            return new KafkaEventPublisher<PhotoCreatedEvent>(producer, "photo-created");
        });

        services.AddSingleton<IDomainEventPublisher<IDomainEvent>, DomainEventPublisher<IDomainEvent>>();

        return services;
    }

    public static IServiceCollection ConfigureReadRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IOrderReadRepository, OrderReadRepository>()
            .AddScoped<IFamilyReadRepository, FamilyReadRepository>()
            .AddScoped<IGenusReadRepository, GenusReadRepository>()
            .AddScoped<ISpeciesReadRepository, SpeciesReadRepository>()
            .AddScoped<IPhotoReadRepository, PhotoReadRepository>();
    }

    public static IServiceCollection ConfigureWriteRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IOrderWriteRepository, OrderWriteRepository>()
            .AddScoped<IFamilyWriteRepository, FamilyWriteRepository>()
            .AddScoped<IGenusWriteRepository, GenusWriteRepository>()
            .AddScoped<ISpeciesWriteRepository, SpeciesWriteRepository>()
            .AddScoped<IPhotoWriteRepository, PhotoWriteRepository>();
    }
}
