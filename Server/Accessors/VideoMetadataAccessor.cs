using System.Text.RegularExpressions;

namespace MyBirds.Server.Accessors;

public static class VideoMetadataAccessor
{
    //private static readonly Regex FileDateRegex = new(@"(?:IMG_)?(\d{4})(\d{2})(\d{2})_(\d{2})(\d{2})(\d{2})\.\w+", RegexOptions.Compiled);
    private static readonly Regex FileDateRegex = new(@"(?:IMG_)?(\d{4})(\d{2})(\d{2})_(\d{2})(\d{2})(\d{2})(?:_\d+)?\.\w+", RegexOptions.Compiled);

    public static DateTime? GetVideoCreatedDate(string videoPath)
    {
        var videoName = videoPath.Split("\\").Last();
        var match = FileDateRegex.Match(videoName);

        if (!match.Success)
            return null;
        
        return new DateTime(
            int.Parse(match.Groups[1].Value),
            int.Parse(match.Groups[2].Value),
            int.Parse(match.Groups[3].Value),
            int.Parse(match.Groups[4].Value),
            int.Parse(match.Groups[5].Value),
            int.Parse(match.Groups[6].Value)
        );
    }
}
