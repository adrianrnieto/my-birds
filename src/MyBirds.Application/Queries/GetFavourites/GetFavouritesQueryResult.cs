using MyBirds.Domain.Photography.Entities;
using MyBirds.Domain.Taxonomy.Entities;

namespace MyBirds.Application.Queries.GetFavourites;

public record class GetFavouritesQueryResult : IQueryResult
{
    public IEnumerable<(Species Species, Photo? Photo)> SpeciesAndPhotosData { get; set; } = default!;
}