namespace MyBirds.Client.Helpers;

public static class PhotoLocator
{
    public static string GetPhotoUrl(string? photoUrl, string? thumbnailUrl)
    {
        if (!string.IsNullOrWhiteSpace(thumbnailUrl))
            return $"/thumbnails/{thumbnailUrl}";

        return photoUrl is not null
            ? $"/photos/{photoUrl}"
            : string.Empty;
    }
}
