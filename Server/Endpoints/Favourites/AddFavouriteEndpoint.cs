using FastEndpoints;
using MyBirds.Application.Abstract;
using MyBirds.Application.Commands.AddFavouritePhoto;
using MyBirds.Shared.Requests;

namespace MyBirds.Server.Endpoints.Favourites;

public class AddFavouriteEndpoint(IAsyncCommandHandler<AddFavouritePhotoCommand> addFavouritePhotoCommandHandler)
    : Endpoint<AddFavouriteRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/favourites");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddFavouriteRequest req, CancellationToken ct)
    {
        var command = new AddFavouritePhotoCommand(req.PhotoId);
        await addFavouritePhotoCommandHandler.HandleAsync(command, ct);
        await Task.CompletedTask;
    }
}
