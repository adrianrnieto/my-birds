using MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;
using MyBirds.Application.Services.Files;
using MyBirds.Domain.Photography.Repositories;

namespace MyBirds.Application.Commands.ScanPhotos;

internal class ScanPhotosCommandHandler(
    IFileSystemScanner fileSystemScanner,
    IPhotoReadRepository photoReadRepository,
    IAsyncCommandHandler<RegisterPhotosAndTaxonomyCommand> registerPhotosAndTaxonomyCommandHandler)
    : IAsyncCommandHandler<ScanPhotosCommand>
{
    private const int _batchSize = 500;

    public async Task HandleAsync(ScanPhotosCommand command, CancellationToken cancellationToken)
    {
        var photos = fileSystemScanner.GetAllFilesInFolder(command.FolderPath);
        foreach (var batch in photos.Chunk(_batchSize))
        {
            var newPhotos = await photoReadRepository.GetMissingByNamesAsync(batch, cancellationToken);
            if (newPhotos is not null)
            {
                var registerPhotosAndTaxonomyCommand = new RegisterPhotosAndTaxonomyCommand { PhotoPaths = newPhotos, BasePath = command.FolderPath };
                await registerPhotosAndTaxonomyCommandHandler.HandleAsync(registerPhotosAndTaxonomyCommand, cancellationToken);
            }
        }
    }
}
