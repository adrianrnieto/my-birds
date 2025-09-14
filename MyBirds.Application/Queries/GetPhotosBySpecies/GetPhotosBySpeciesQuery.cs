using MyBirds.Application.Abstract;

namespace MyBirds.Application.Queries.GetPhotosBySpecies;

public record GetPhotosBySpeciesQuery(int SpeciesId) : IQuery { }
