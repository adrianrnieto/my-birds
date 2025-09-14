namespace MyBirds.Shared.ViewModels;

public record FavouritesViewModel
{
    public required string ScientificName { get; set; }
    public required string SpeciesName { get; set; }
    public required int SpeciesId { get; set; }
    public string? PhotoUrl { get; set; }
    public bool IsStarred { get; set; }
}
