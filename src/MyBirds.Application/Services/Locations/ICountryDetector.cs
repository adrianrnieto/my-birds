using MyBirds.Shared;

namespace MyBirds.Application.Services.Locations;

public interface ICountryDetector
{
    Country DetectFromFile(string filePath);
    Country DetectFromImage(string imagePath);
}
