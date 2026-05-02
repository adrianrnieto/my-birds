namespace MyBirds.Application.Services.Files;

public interface IVideoMetadataAccessor
{
    DateTime? GetVideoCreatedDate(string videoPath);
}
