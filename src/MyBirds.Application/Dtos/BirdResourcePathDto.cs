namespace MyBirds.Application.Dtos;

internal record BirdResourcePathDto
{
    public required string OrderName { get; init; }
    public required string FamilyName { get; init; }
    public required string GenusName { get; init; }
    public required string SpeciesName { get; init; }
    public required string SpeciesScientificName { get; init; }
    public required string ResourceName { get; init; }
}
