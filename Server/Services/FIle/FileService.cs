namespace CMVMD.Server.Services.File;

public class FileService : IFileService
{
    const string FolderPath = "/Users/petrugrigor/Documents/VetSite/CMVMD/Server/Files";

    public async Task<string> Add(IFormFile file)
    {
        var folderPath = FolderPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        var filePath = Path.Combine(folderPath, file.FileName);

        var directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath!);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    public Task Delete(string path)
    {
        throw new NotImplementedException();
    }

    public Task<IFormFile> Get(string path)
    {
        throw new NotImplementedException();
    }
}
