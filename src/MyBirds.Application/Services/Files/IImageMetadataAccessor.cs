using MyBirds.Application.Models;

namespace MyBirds.Application.Services.Files;

public interface IImageMetadataAccessor
{
    ImageMetadata GetMetadata(string imagePath);
}
