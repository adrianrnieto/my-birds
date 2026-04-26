namespace MyBirds.Server.Options;

public class PhotoStorageOptions
{
    public const string SectionName = "PhotoStorage";

    public string PhotoRootPath { get; set; } = string.Empty;
    public string ThumbnailOutputFolderName { get; set; } = "thumbnails-cache";
    public int ThumbnailSize { get; set; } = 360;
    public int PhotosCacheMaxAgeDays { get; set; } = 7;
    public int ThumbnailsCacheMaxAgeDays { get; set; } = 30;
    public int ThumbnailGenerationIntervalSeconds { get; set; } = 300;
    public int MaxFilesPerRun { get; set; } = 0;
}
