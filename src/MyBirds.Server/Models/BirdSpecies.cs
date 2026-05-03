using MyBirds.Shared;

namespace MyBirds.Server.Models;

public class BirdSpecies
{
    public string Name { get; init; } = default!;
    public string Species { get; init; } = default!;
    public string Genus { get; init; } = default!;
    public string Family { get; init; } = default!;
    public string Order { get; init; } = default!;
    public int PicturesCount { get; init; }
    public IEnumerable<Country> Countries { get; init; } = default!;
}
