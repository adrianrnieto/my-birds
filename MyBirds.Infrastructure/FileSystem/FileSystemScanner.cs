using MyBirds.Application.Services;

namespace MyBirds.Infrastructure.FileSystem;

public class FileSystemScanner : IFileSystemScanner
{
    public IEnumerable<string> GetAllFilesInFolder(string baseFolderPath)
    {
        List<string> files = [];
        var directories = Directory.GetDirectories(baseFolderPath);
        if (directories.Length == 0)
        {
            return Directory.GetFiles(baseFolderPath);
        }
        else
        {
            foreach (var directory in directories)
            {
                files.AddRange(GetAllFilesInFolder(directory));
            }
        }
        return files;
    }
}
