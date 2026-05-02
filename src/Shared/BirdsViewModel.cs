namespace MyBirds.Shared;

public record OrderGroupViewModel
{
    public string Order { get; init; } = default!;
    public IEnumerable<FamilyGroupViewModel> Families { get; init; } = default!;
}

public record FamilyGroupViewModel
{
    public string Family { get; init; } = default!;
    public IEnumerable<GenusGroupViewModel> Genera { get; init; } = default!;
}

public record GenusGroupViewModel
{
    public string Genus { get; init; } = default!;
    public IEnumerable<SpeciesViewModel> Species { get; init; } = default!;
}

public record SpeciesViewModel
{
    public string Name { get; init; } = default!;
    public string Species { get; init; } = default!;
    public int PicturesCount { get; init; } = default!;
    public IEnumerable<Country> Countries { get; init; } = default!;
}