using MyBirds.Shared;

namespace MyBirds.Infrastructure.Database.Entities;

internal class Bird : Entity
{
    public string Name { get; init; } = default!;
    public string Species { get; init; } = default!;
    public string Genus { get; init; } = default!;
    public string Family { get; init; } = default!;
    public string Order { get; init; } = default!;
    public int PicturesCount { get; init; }
    public IEnumerable<Country> Countries { get; init; } = default!;
}
