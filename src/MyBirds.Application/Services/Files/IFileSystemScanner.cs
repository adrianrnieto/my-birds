namespace MyBirds.Application.Services.Files;

public interface IFileSystemScanner
{
    IEnumerable<string> GetAllFilesInFolder(string baseFolderPath);
}
