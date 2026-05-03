using MyBirds.Domain.Photography.Entities;

namespace MyBirds.Application.Queries.GetPhotosBySpecies;

public record class GetPhotosBySpeciesQueryResult(IEnumerable<Photo> Photos) : IQueryResult { }
