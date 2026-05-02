namespace MyBirds.Application.Services.Thumbnails;

public record ThumbnailBatchResult(int Scanned, int Generated, int Skipped, int Unsupported, int Failed);
