namespace MyBirds.Shared.ViewModels;

public record AllPhotosBySpeciesViewModel
{
    public required int PhotoId { get; set; }
    public required string PhotoUrl { get; set; }
    public required bool IsFavourite { get; set; }
    public required bool IsStarred { get; set; }
}
