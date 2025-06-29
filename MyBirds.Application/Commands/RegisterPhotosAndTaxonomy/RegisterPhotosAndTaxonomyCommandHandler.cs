using MyBirds.Application.Abstract;
using MyBirds.Domain.Birds;

namespace MyBirds.Application.Commands.RegisterPhotosAndTaxonomy;

public class RegisterPhotosAndTaxonomyCommandHandler(IPhotoRepository photoRepository) : IAsyncCommandHandler<RegisterPhotosAndTaxonomyCommand>
{
    private readonly IPhotoRepository _photoRepository = photoRepository;

    public async Task HandleAsync(RegisterPhotosAndTaxonomyCommand command, CancellationToken cancellationToken)
    {
        // TODO: Check if Order exists - persist
        // TODO: Check if Family exists - persist
        // TODO: Check if Genus exists - persist
        // TODO: Check if Species exists - persist
        // TODO: Get image metadata
        // TODO: Persist photo
        throw new NotImplementedException();
    }
}
