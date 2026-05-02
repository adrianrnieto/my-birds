using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

public interface ICameraToCountryResolver
{
    Country ResolveByCamera(string cameraMaker);
}
