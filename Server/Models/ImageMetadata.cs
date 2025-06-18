namespace MyBirds.Server.Models;

public record ImageMetadata
{
    public ImageMetadataLocation? Location { get; init; }
    public DateTime? CreationDate { get; init; }
    public string? CameraMaker { get; init; }
    public string? CameraModel { get; init; }
}

public record ImageMetadataLocation
{
    public ImageMetadataLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; init; }
    public double Longitude { get; init; }
}
