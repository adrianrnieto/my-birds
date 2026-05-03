namespace MyBirds.Application.Commands.ScanPhotos;

public record class ScanPhotosCommand : ICommand
{
    public required string FolderPath { get; set; }
}
