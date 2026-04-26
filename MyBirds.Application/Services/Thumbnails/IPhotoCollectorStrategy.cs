namespace MyBirds.Application.Services.Thumbnails;

internal interface IPhotoCollectorStrategy
{
    Task<IEnumerable<string>> CollectAsync(CancellationToken cancellationToken);
}
