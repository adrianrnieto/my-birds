namespace MyBirds.Application.Services.Files;

public interface IImageEditor
{
    Task CopyAndResizeAsync(string originalPhotoPath, string newPhotoPath, int newImageSize, CancellationToken cancellationToken);
}
