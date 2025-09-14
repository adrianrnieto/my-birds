using MyBirds.Application.Abstract;

namespace MyBirds.Application.Commands.AddFavouritePhoto;

public record AddFavouritePhotoCommand(int PhotoId) : ICommand { }
