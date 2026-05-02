using MyBirds.Shared;
using MyBirds.Application.Services.Files;

namespace MyBirds.Application.Services.Locations;

public class CountryDetector(
    IVideoMetadataAccessor videoMetadataAccessor,
    IImageMetadataAccessor imageMetadataAccessor,
    ILocationToCountryResolver locationResolver,
    IDateToCountryResolver dateResolver,
    ICameraToCountryResolver cameraResolver)
    : ICountryDetector
{
    public Country DetectFromFile(string filePath)
    {
        if (filePath.EndsWith(".mp4"))
        {
            var creationDate = videoMetadataAccessor.GetVideoCreatedDate(filePath);
            if (creationDate is null)
                return Country.Unknown;

            return dateResolver.ResolveByCreationDate(creationDate.Value);
        }

        return DetectFromImage(filePath);
    }

    public Country DetectFromImage(string imagePath)
    {
        var metadata = imageMetadataAccessor.GetMetadata(imagePath);

        // Priority 1: Geographic location
        if (metadata.Location != null)
            return locationResolver.ResolveByLocation(metadata.Location.Latitude, metadata.Location.Longitude);

        // Priority 2: Creation date
        if (metadata.CreationDate is not null)
        {
            var country = dateResolver.ResolveByCreationDate(metadata.CreationDate.Value);
            if (country != Country.Unknown)
            {
                return country;
            }
        }
        else
        {
            var datetimeFromFilename = videoMetadataAccessor.GetVideoCreatedDate(imagePath);
            if (datetimeFromFilename is not null)
            {
                var country = dateResolver.ResolveByCreationDate(datetimeFromFilename.Value);
                if (country != Country.Unknown)
                {
                    return country;
                }
            }
        }

        // Priority 3: Camera maker
        if (metadata.CameraMaker is not null)
        {
            var country = cameraResolver.ResolveByCamera(metadata.CameraMaker);
            if (country != Country.Unknown)
            {
                return country;
            }
        }

        return Country.Unknown;
    }
}
