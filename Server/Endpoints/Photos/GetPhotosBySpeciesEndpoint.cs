using FastEndpoints;
using MyBirds.Application.Abstract;
using MyBirds.Application.Queries.GetPhotosBySpecies;
using MyBirds.Application.Services.Thumbnails;
using MyBirds.Shared.ViewModels;

namespace MyBirds.Server.Endpoints.Photos;

public class GetPhotosBySpeciesRequest
{
    public int SpeciesId { get; set; }
}

public class GetPhotosBySpeciesEndpoint(
    IAsyncQueryHandler<GetPhotosBySpeciesQuery, GetPhotosBySpeciesQueryResult> getPhotosBySpeciesQueryHandler,
    IThumbnailPathService thumbnailPathService)
    : Endpoint<GetPhotosBySpeciesRequest, IEnumerable<AllPhotosBySpeciesViewModel>>
{
    public override void Configure()
    {
        Get("/photos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetPhotosBySpeciesRequest req, CancellationToken ct)
    {
        var query = new GetPhotosBySpeciesQuery(req.SpeciesId);
        var result = await getPhotosBySpeciesQueryHandler.HandleAsync(query, ct);

        Response = result.Value!.Photos.Select(p => new AllPhotosBySpeciesViewModel
        {
            PhotoId = p.Id,
            PhotoUrl = p.FullPath,
            ThumbnailUrl = thumbnailPathService.GetThumbnailRelativePath(p.FullPath),
            IsStarred = p.IsStarred,
            IsFavourite = p.IsFavourite
        }).ToList();
    }
}
