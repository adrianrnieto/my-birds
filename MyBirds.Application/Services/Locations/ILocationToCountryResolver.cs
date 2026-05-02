using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

public interface ILocationToCountryResolver
{
    Country ResolveByLocation(double latitude, double longitude);
}
