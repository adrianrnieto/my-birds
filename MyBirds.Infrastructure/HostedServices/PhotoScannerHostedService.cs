using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyBirds.Application.Abstract;
using MyBirds.Application.Commands.ScanPhotos;
using MyBirds.Application.Options;

namespace MyBirds.Infrastructure.HostedServices;

public class PhotoScannerHostedService(IServiceProvider serviceProvider, IOptions<PhotoStorageOptions> options)
    : BackgroundService
{
    private readonly string _photoRootPath = Path.GetFullPath(options.Value.PhotoRootPath);

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: Check if enabled

        if (!Directory.Exists(_photoRootPath))
            throw new DirectoryNotFoundException($"Directoy {_photoRootPath} does not exist.");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var scanPhotosCommandHandler = scope.ServiceProvider.GetRequiredService<IAsyncCommandHandler<ScanPhotosCommand>>();
            var scanPhotosCommand = new ScanPhotosCommand { FolderPath = _photoRootPath };
            await scanPhotosCommandHandler.HandleAsync(scanPhotosCommand, stoppingToken);

            // TODO: Get delay from config
            await Task.Delay(60000, stoppingToken);
        }
    }
}