using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

public record GeographicBoundary(double MinLatitude, double MaxLatitude, double MinLongitude, double MaxLongitude, Country Country);
