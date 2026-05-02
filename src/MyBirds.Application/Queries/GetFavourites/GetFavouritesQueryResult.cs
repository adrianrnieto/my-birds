using MyBirds.Application.Abstract;
using MyBirds.Domain.Birds;

namespace MyBirds.Application.Queries.GetFavourites;

public record class GetFavouritesQueryResult : IQueryResult
{
    public IEnumerable<(Species Species, Photo? Photo)> SpeciesAndPhotosData { get; set; } = default!;
}