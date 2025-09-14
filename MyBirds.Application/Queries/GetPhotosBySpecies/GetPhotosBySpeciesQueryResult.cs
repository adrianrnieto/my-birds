using MyBirds.Application.Abstract;
using MyBirds.Domain.Birds;

namespace MyBirds.Application.Queries.GetPhotosBySpecies;

public record class GetPhotosBySpeciesQueryResult(IEnumerable<Photo> Photos) : IQueryResult { }
