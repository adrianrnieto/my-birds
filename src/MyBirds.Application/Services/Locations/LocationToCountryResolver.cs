using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

public interface ILocationToCountryResolver
{
    Country ResolveByLocation(double latitude, double longitude);
}


public class LocationToCountryResolver : ILocationToCountryResolver
{
    private static readonly GeographicBoundary[] GeographicBoundaries =
    {
        new(35.0, 44.5, -9.5, 3.5, Country.Spain),
        new(50.7, 53.6, 3.2, 7.2, Country.Netherlands),
        new(8.2, 23.4, 102.1, 109.5, Country.Vietnam),
        new(5.6, 20.5, 97.3, 105.6, Country.Thailand),
        new(17.5, 19.9, -71.9, -68.3, Country.DominicanRepublic),
        new(14.5, 32.7, -118.5, -86.7, Country.Mexico),
        new(-5.0, 5.3, 33.5, 42.1, Country.Kenya),
        new(4.6, 21.3, 116.9, 126.6, Country.Philippines),
        new(48.5, 51.1, 12.0, 18.9, Country.CzechRepublic),
    };

    public Country ResolveByLocation(double latitude, double longitude)
    {
        foreach (var boundary in GeographicBoundaries)
        {
            if (latitude >= boundary.MinLatitude && latitude <= boundary.MaxLatitude &&
                longitude >= boundary.MinLongitude && longitude <= boundary.MaxLongitude)
            {
                return boundary.Country;
            }
        }

        return Country.Unknown;
    }
}
