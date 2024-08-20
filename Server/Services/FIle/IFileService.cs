namespace CMVMD.Server.Services.File;

public interface IFileService
{
    Task<string> Add(IFormFile file);
}
