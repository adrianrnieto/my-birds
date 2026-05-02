using FastEndpoints;
using MyBirds.Application.Abstract;
using MyBirds.Application.Queries.GetFavourites;
using MyBirds.Application.Services.Thumbnails;
using MyBirds.Shared.ViewModels;

namespace MyBirds.Server.Endpoints.Favourites;

public class GetFavouritesRequest
{
    public string? Order { get; set; }
    public string? Family { get; set; }
}

public class GetFavouritesEndpoint(
    IAsyncQueryHandler<GetFavouritesQueryResult> getFavouritesQueryHandler,
    IThumbnailPathService thumbnailPathService)
    : Endpoint<GetFavouritesRequest, IEnumerable<FavouritesViewModel>>
{
    public override void Configure()
    {
        Get("/favourites");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetFavouritesRequest req, CancellationToken ct)
    {
        var favourites = await getFavouritesQueryHandler.HandleAsync(ct);
        if (favourites.IsFailure)
        {
            ThrowError(favourites.Error ?? "An error occurred");
        }

        Response = favourites.Value!.SpeciesAndPhotosData
            .Where(data => data.Species.Genus?.Family?.Order != null)
            .Select(data => new FavouritesViewModel
            {
                PhotoUrl = data.Photo?.FullPath,
                ThumbnailUrl = thumbnailPathService.GetThumbnailRelativePath(data.Photo?.FullPath),
                ScientificName = data.Species.ScientificName,
                SpeciesName = data.Species.Name,
                SpeciesId = data.Species.Id,
                IsStarred = data.Photo?.IsStarred ?? false,
                Order = data.Species.Genus?.Family?.Order?.Name ?? string.Empty,
                Family = data.Species.Genus?.Family?.Name ?? string.Empty
            })
            .Where(vm => string.IsNullOrEmpty(req.Order) || vm.Order == req.Order)
            .Where(vm => string.IsNullOrEmpty(req.Family) || vm.Family == req.Family)
            .OrderBy(vm => vm.ScientificName)
            .ToList();
        await Task.CompletedTask;
    }

    private void ThrowError(string error)
    {
        throw new InvalidOperationException(error);
    }
}
