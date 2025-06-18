using MyBirds.Shared;
using MyBirds.Server.Accessors;

namespace MyBirds.Server.Providers;

public static class CountryProvider
{
    public static Country GetCountryFromFile(string filePath)
    {
        if (filePath.EndsWith(".mp4"))
        {
            var creationDate = VideoMetadataAccessor.GetVideoCreatedDate(filePath);
            if (creationDate is null)
                return Country.Unknown;

            return GetCountryByCreationDate(creationDate.Value);

        }

        return GetCountryFromImage(filePath);
    }

    public static Country GetCountryFromImage(string imagePath)
    {
        var metadata = ImageMetadataAccessor.GetMetadata(imagePath);

        if (metadata.Location != null)
            return GetCountryByLocation(metadata.Location.Latitude, metadata.Location.Longitude);

        if (metadata.CreationDate is not null)
        {
            var country = GetCountryByCreationDate(metadata.CreationDate.Value);
            if (country != Country.Unknown)
            {
                return country;
            }
        }
        else
        {
            var datetimeFromFilename = VideoMetadataAccessor.GetVideoCreatedDate(imagePath);
            if (datetimeFromFilename is not null)
            {
                var country = GetCountryByCreationDate(datetimeFromFilename.Value);
                if (country != Country.Unknown)
                {
                    return country;
                }
            }
        }

        if (metadata.CameraMaker is not null)
        {
            var country = GetCountryByCamera(metadata.CameraMaker);
            if (country != Country.Unknown)
            {
                return country;
            }
        }

        return Country.Unknown;
    }

    private static Country GetCountryByLocation(double latitude, double longitude)
    {
        if (latitude >= 35.0 && latitude <= 44.5 &&
            longitude >= -9.5 && longitude <= 3.5)
            return Country.Spain;

        if (latitude >= 50.7 && latitude <= 53.6 &&
            longitude >= 3.2 && longitude <= 7.2)
            return Country.Netherlands;

        if (latitude >= 8.2 && latitude <= 23.4 &&
            longitude >= 102.1 && longitude <= 109.5)
            return Country.Vietnam;

        if (latitude >= 5.6 && latitude <= 20.5 &&
            longitude >= 97.3 && longitude <= 105.6)
            return Country.Thailand;

        if (latitude >= 17.5 && latitude <= 19.9 &&
            longitude >= -71.9 && longitude <= -68.3)
            return Country.DominicanRepublic;

        if (latitude >= 14.5 && latitude <= 32.7 &&
            longitude >= -118.5 && longitude <= -86.7)
            return Country.Mexico;

        if (latitude >= -5.0 && latitude <= 5.3 &&
            longitude >= 33.5 && longitude <= 42.1)
            return Country.Kenya;

        if (latitude >= 4.6 && latitude <= 21.3 &&
            longitude >= 116.9 && longitude <= 126.6)
            return Country.Philippines;

        if (latitude >= 48.5 && latitude <= 51.1 &&
            longitude >= 12.0 && longitude <= 18.9)
            return Country.CzechRepublic;

        return Country.Unknown;
    }

    private static Country GetCountryByCamera(string cameraMaker)
    {
        return cameraMaker switch
        {
            "Panasonic" => Country.Spain,
            _ => Country.Unknown,
        };
    }

    private static Country GetCountryByCreationDate(DateTime creationDate)
    {
        if (creationDate >= new DateTime(2013, 9, 14) && creationDate <= new DateTime(2013, 9, 21, 23, 59, 59))
            return Country.DominicanRepublic;

        if (creationDate >= new DateTime(2016, 5, 13) && creationDate <= new DateTime(2016, 5, 23, 23, 59, 59))
            return Country.Mexico;

        if (creationDate >= new DateTime(2017, 4, 20) && creationDate <= new DateTime(2017, 4, 27, 23, 59, 59))
            return Country.Germany;

        if (creationDate >= new DateTime(2017, 4, 20) && creationDate <= new DateTime(2017, 4, 27, 23, 59, 59))
            return Country.Germany;

        if (creationDate >= new DateTime(2017, 11, 11) && creationDate <= new DateTime(2017, 11, 20, 23, 59, 59))
            return Country.CzechRepublic;

        if (creationDate >= new DateTime(2018, 9, 1) && creationDate <= new DateTime(2018, 9, 10, 23, 59, 59))
            return Country.Germany;

        if (creationDate >= new DateTime(2018, 9, 11) && creationDate <= new DateTime(2018, 9, 15, 23, 59, 59))
            return Country.Belgium;

        if (creationDate >= new DateTime(2018, 10, 5) && creationDate <= new DateTime(2018, 10, 7, 23, 59, 59))
            return Country.France;

        if (creationDate >= new DateTime(2019, 5, 1) && creationDate <= new DateTime(2019, 5, 6, 23, 59, 59))
            return Country.France;

        if (creationDate >= new DateTime(2019, 6, 20) && creationDate <= new DateTime(2019, 6, 22, 23, 59, 59))
            return Country.Singapore;

        if (creationDate >= new DateTime(2019, 6, 23) && creationDate <= new DateTime(2019, 7, 14, 23, 59, 59))
            return Country.Indonesia;

        if (creationDate >= new DateTime(2019, 7, 15) && creationDate <= new DateTime(2019, 7, 27, 23, 59, 59))
            return Country.SriLanka;

        if (creationDate >= new DateTime(2021, 9, 5) && creationDate <= new DateTime(2021, 9, 13, 23, 59, 59))
            return Country.Kenya;

        if (creationDate >= new DateTime(2023, 3, 19) && creationDate <= new DateTime(2023, 4, 11, 23, 59, 59))
            return Country.Thailand;

        if (creationDate >= new DateTime(2024, 1, 5) && creationDate <= new DateTime(2024, 1, 25, 23, 59, 59))
            return Country.Philippines;

        if (creationDate >= new DateTime(2024, 1, 26) && creationDate <= new DateTime(2024, 2, 24, 23, 59, 59))
            return Country.Thailand;

        if (creationDate >= new DateTime(2024, 2, 25) && creationDate <= new DateTime(2024, 4, 9, 23, 59, 59))
            return Country.Vietnam;

        if (creationDate >= new DateTime(2024, 4, 10) && creationDate <= new DateTime(2024, 4, 13, 23, 59, 59))
            return Country.Netherlands;

        return Country.Spain;
    }
}
