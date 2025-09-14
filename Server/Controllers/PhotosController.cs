using Microsoft.AspNetCore.Mvc;
using MyBirds.Application.Abstract;
using MyBirds.Application.Queries.GetPhotosBySpecies;
using MyBirds.Shared.ViewModels;

namespace MyBirds.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotosController(IAsyncQueryHandler<GetPhotosBySpeciesQuery, GetPhotosBySpeciesQueryResult> getPhotosBySpeciesQueryHandler)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int speciesId)
    {
        var query = new GetPhotosBySpeciesQuery(speciesId);
        var result = await getPhotosBySpeciesQueryHandler.HandleAsync(query, CancellationToken.None);

        var viewModel = result.Value!.Photos.Select(p => new AllPhotosBySpeciesViewModel
        {
            PhotoId = p.Id,
            PhotoUrl = p.FullPath,
            IsStarred = p.IsStarred,
            IsFavourite = p.IsFavourite
        });

        return Ok(viewModel);
    }
}
