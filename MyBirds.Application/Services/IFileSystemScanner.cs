namespace MyBirds.Application.Services;

public interface IFileSystemScanner
{
    IEnumerable<string> GetAllFilesInFolder(string baseFolderPath);
}
