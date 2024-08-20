namespace CMVMD.Server.Services.File;

public class FileService : IFileService
{
    const string FolderPath = "/Users/petrugrigor/Documents/VetSite/CMVMD/Server/Files";

    public async Task<string> Add(IFormFile file)
    {
        var folderPath = FolderPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        var uniqueFileName = GenerateUniqueFileName(file.FileName);

        var filePath = Path.Combine(folderPath, uniqueFileName);

        var directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath!);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName;
    }

    private string GenerateUniqueFileName(string originalFileName)
    {
        var fileExtension = Path.GetExtension(originalFileName);

        var uniqueIdentifier = Guid.NewGuid().ToString();

        var uniqueFileName = $"{uniqueIdentifier}{fileExtension}";

        return uniqueFileName;
    }
}
