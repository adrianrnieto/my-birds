using MyBirds.Domain.Photography.Entities;
using MyBirds.Domain.Photography.Repositories;

namespace MyBirds.Application.Commands.AddFavouritePhoto;

public class AddFavouritePhotoCommandHandler(IPhotoReadRepository photoReadRepository, IPhotoWriteRepository photoWriteRepository)
    : IAsyncCommandHandler<AddFavouritePhotoCommand>
{
    public async Task HandleAsync(AddFavouritePhotoCommand command, CancellationToken cancellationToken)
    {
        var photo = await photoReadRepository.GetByIdAsync(command.PhotoId, cancellationToken)
            ?? throw new ArgumentException(nameof(command.PhotoId), $"Photo with id '{command.PhotoId}' does not exist");

        photo.IsFavourite = true;

        IList<Photo> photosToUpdate = [photo];

        var previousFavourite = await photoReadRepository.GetFavouriteBySpeciesAsync(photo.SpeciesId, cancellationToken);
        if (previousFavourite is not null)
        {
            previousFavourite.IsFavourite = false;
            photosToUpdate.Add(previousFavourite);
        }

        await photoWriteRepository.UpdateAsync(photosToUpdate, cancellationToken);

        // TODO: Generate thumbnail for the photo if it does not exist yet
    }
}
