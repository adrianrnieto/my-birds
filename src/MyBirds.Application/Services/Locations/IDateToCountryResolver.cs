using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

public interface IDateToCountryResolver
{
    Country ResolveByCreationDate(DateTime creationDate);
}
