namespace MyBirds.Application.Models;

public record ImageMetadata
{
    public ImageMetadataLocation? Location { get; init; }
    public DateTime? CreationDate { get; init; }
    public string? CameraMaker { get; init; }
    public string? CameraModel { get; init; }
}

public record ImageMetadataLocation(double Latitude, double Longitude);
