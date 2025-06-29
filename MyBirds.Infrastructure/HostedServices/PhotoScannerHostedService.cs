using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBirds.Application.Abstract;
using MyBirds.Application.Commands.ScanPhotos;

namespace MyBirds.Infrastructure.HostedServices;

public class PhotoScannerHostedService(IServiceProvider serviceProvider) : BackgroundService
{
    private const string _path = @"C:\Users\Adrian\Pictures\FX82\Birds";

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: Check if enabled

        if (!Directory.Exists(_path))
            throw new ApplicationException("El directorio no existe.");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var scanPhotosCommandHandler = scope.ServiceProvider.GetRequiredService<IAsyncCommandHandler<ScanPhotosCommand>>();
            var scanPhotosCommand = new ScanPhotosCommand { FolderPath = _path };
            await scanPhotosCommandHandler.HandleAsync(scanPhotosCommand, stoppingToken);

            // TODO: Get delay from config
            await Task.Delay(60000, stoppingToken);
        }
    }
}
