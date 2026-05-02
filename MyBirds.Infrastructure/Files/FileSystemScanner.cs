using MyBirds.Application.Services.Files;

namespace MyBirds.Infrastructure.Files;

internal class FileSystemScanner : IFileSystemScanner
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
