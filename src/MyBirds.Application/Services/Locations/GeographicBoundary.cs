using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

internal record GeographicBoundary(double MinLatitude, double MaxLatitude, double MinLongitude, double MaxLongitude, Country Country);
