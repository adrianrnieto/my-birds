using MyBirds.Infrastructure.Database.Abstract;
using MyBirds.Shared;

namespace MyBirds.Infrastructure.Database.Entities;

internal partial class Photo : Entity
{
    public required string Name { get; set; }
    public Country Country { get; set; }
    public required int SpeciesId { get; set; }
    public virtual Species? Species { get; set; }
}
