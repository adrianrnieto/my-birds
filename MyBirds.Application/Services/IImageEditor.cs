namespace MyBirds.Application.Services;

public interface IImageEditor
{
    Task CopyAndResizeAsync(string originalPhotoPath, string newPhotoPath, int newImageSize, CancellationToken cancellationToken);
}
