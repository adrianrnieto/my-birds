using MyBirds.Application.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace MyBirds.Infrastructure.Images;

internal class ImageEditor : IImageEditor
{
    public async Task CopyAndResizeAsync(string originalPhotoPath, string newPhotoPath, int newImageSize, CancellationToken cancellationToken)
    {
        try
        {
            using var image = await Image.LoadAsync(originalPhotoPath, cancellationToken);
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(newImageSize, newImageSize),
                Mode = ResizeMode.Crop,
                Position = AnchorPositionMode.Center
            }));
            await image.SaveAsync(newPhotoPath, cancellationToken);
        }
        catch (UnknownImageFormatException ex)
        {
            throw new NotSupportedException($"The file format of {originalPhotoPath} is not supported.", ex);
        }
    }
}
