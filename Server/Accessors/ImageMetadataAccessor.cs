using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MyBirds.Server.Models;

namespace MyBirds.Server.Accessors;

public static class ImageMetadataAccessor
{
    public static ImageMetadata GetMetadata(string imagePath)
    {
        var metadata = ImageMetadataReader.ReadMetadata(imagePath);

        return new()
        {
            Location = GetLocation(metadata),
            CreationDate = GetCreationDate(metadata),
            CameraMaker = GetCameraMaker(metadata)
        };
    }

    private static ImageMetadataLocation? GetLocation(IReadOnlyList<MetadataExtractor.Directory> metadata)
    {
        var gps = metadata.OfType<GpsDirectory>().FirstOrDefault();

        var location = gps?.GetGeoLocation();

        return location != null
            ? new ImageMetadataLocation(location.Latitude, location.Longitude)
            : null;
    }

    private static DateTime? GetCreationDate(IReadOnlyList<MetadataExtractor.Directory> metadata)
    {
        var subIfdDirectory = metadata.OfType<ExifSubIfdDirectory>().FirstOrDefault();

        if (subIfdDirectory != null && subIfdDirectory.TryGetDateTime(ExifDirectoryBase.TagDateTimeOriginal, out var date))
        {
            return date;
        }

        var ifd0Directory = metadata.OfType<ExifIfd0Directory>().FirstOrDefault();
        if (ifd0Directory != null && ifd0Directory.TryGetDateTime(ExifDirectoryBase.TagDateTime, out date))
        {
            return date;
        }

        return null;
    }

    public static string? GetCameraMaker(IReadOnlyList<MetadataExtractor.Directory> metadata)
    {
        var ifd0 = metadata.OfType<ExifIfd0Directory>().FirstOrDefault();

        if (ifd0 != null && ifd0.ContainsTag(ExifDirectoryBase.TagMake))
        {
            return ifd0.GetDescription(ExifDirectoryBase.TagMake);
        }

        return null;
    }

    // GetCameraModel

}
