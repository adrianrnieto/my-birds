using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

public interface ICameraToCountryResolver
{
    Country ResolveByCamera(string cameraMaker);
}

internal class CameraToCountryResolver : ICameraToCountryResolver
{
    private static readonly Dictionary<string, Country> CameraCountryMap = new()
    {
        { "Panasonic", Country.Spain },
    };

    public Country ResolveByCamera(string cameraMaker)
    {
        return CameraCountryMap.TryGetValue(cameraMaker, out var country) 
            ? country 
            : Country.Unknown;
    }
}
