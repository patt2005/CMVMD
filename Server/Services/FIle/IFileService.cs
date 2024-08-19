namespace CMVMD.Server.Services.File;

public interface IFileService
{
    Task<string> Add(IFormFile file);
    Task<IFormFile> Get(string path);
    Task Delete(string path);
}
