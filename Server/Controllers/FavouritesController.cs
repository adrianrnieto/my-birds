using Microsoft.AspNetCore.Mvc;
using MyBirds.Application.Abstract;
using MyBirds.Application.Commands.AddFavouritePhoto;
using MyBirds.Application.Queries.GetFavourites;
using MyBirds.Application.Services.Thumbnails;
using MyBirds.Server.Requests;
using MyBirds.Shared.ViewModels;

namespace MyBirds.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FavouritesController(
    IAsyncQueryHandler<GetFavouritesQueryResult> getFavouritesQueryHandler,
    IAsyncCommandHandler<AddFavouritePhotoCommand> addFavouritePhotoCommandHandler,
    IThumbnailPathService thumbnailPathService)
    : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var favourites = await getFavouritesQueryHandler.HandleAsync(CancellationToken.None);
        if (favourites.IsFailure)
        {
            return Problem(favourites.Error);
        }

        //var random = new Random();
        var viewModel = favourites.Value!.SpeciesAndPhotosData.Select(data => new FavouritesViewModel
        {
            PhotoUrl = data.Photo?.FullPath,
            ThumbnailUrl = thumbnailPathService.GetThumbnailRelativePath(data.Photo?.FullPath),
            ScientificName = data.Species.ScientificName,
            SpeciesName = data.Species.Name,
            SpeciesId = data.Species.Id,
            IsStarred = data.Photo?.IsStarred ?? false
        }).OrderBy(vm => vm.ScientificName);//.OrderBy(vm => random.Next());

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavouriteAsync([FromBody] AddFavouriteRequest request)
    {
        var command = new AddFavouritePhotoCommand(request.PhotoId);
        await addFavouritePhotoCommandHandler.HandleAsync(command, CancellationToken.None);

        return Ok();
    }

}
