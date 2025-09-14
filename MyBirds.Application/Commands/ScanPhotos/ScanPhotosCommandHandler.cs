using MyBirds.Application.Abstract;
using MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;
using MyBirds.Application.Services;
using MyBirds.Domain.Birds;

namespace MyBirds.Application.Commands.ScanPhotos;

internal class ScanPhotosCommandHandler(
    IFileSystemScanner fileSystemScanner,
    IPhotoRepository photoRepository,
    IAsyncCommandHandler<RegisterPhotosAndTaxonomyCommand> registerPhotosAndTaxonomyCommandHandler)
    : IAsyncCommandHandler<ScanPhotosCommand>
{
    private const int _batchSize = 500;

    private readonly IFileSystemScanner _fileSystemScanner = fileSystemScanner;
    private readonly IPhotoRepository _photoRepository = photoRepository;
    private readonly IAsyncCommandHandler<RegisterPhotosAndTaxonomyCommand> _registerPhotosAndTaxonomyCommandHandler = registerPhotosAndTaxonomyCommandHandler;

    public async Task HandleAsync(ScanPhotosCommand command, CancellationToken cancellationToken)
    {
        var photos = _fileSystemScanner.GetAllFilesInFolder(command.FolderPath);
        foreach (var batch in photos.Chunk(_batchSize))
        {
            var newPhotos = await _photoRepository.GetMissingPhotosAsync(batch, cancellationToken);
            if (newPhotos is not null)
            {
                var registerPhotosAndTaxonomyCommand = new RegisterPhotosAndTaxonomyCommand { PhotoPaths = newPhotos, BasePath = command.FolderPath };
                await _registerPhotosAndTaxonomyCommandHandler.HandleAsync(registerPhotosAndTaxonomyCommand, cancellationToken);
            }
        }
    }
}
