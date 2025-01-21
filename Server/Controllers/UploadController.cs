using AutoMapper;
using CMVMD.Server.Services.File;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Persistance.Data;

namespace CMVMD.Server.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly UploadFileConfig _config;

    public UploadController(IFileService fileService, UploadFileConfig config, AppDbContext appDbContext, IMapper mapper)
    {
        _fileService = fileService;
        _appDbContext = appDbContext;
        _mapper = mapper;
        _config = config;
    }

    [HttpPost("image")]
    public async Task<IActionResult> Image(IFormFile file)
    {
        try
        {
            string fileName = await _fileService.Add(file);
            var url = $"{_config.FolderPath}/{fileName}";
            return Ok(new { Url = url });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("file/single")]
    public async Task<FileDto> UploadImage(IFormFile file)
    {
        try
        {
            string fileName = await _fileService.Add(file);
            var newFile = new FileDto()
            {
                FileName = fileName,
                FileUrl = Url.Content($"{_config.FolderPath}/{fileName}"),
                Id = Guid.NewGuid()
            };
            _appDbContext.Files.Add(_mapper.Map<Persistance.Entities.File>(newFile));
            await _appDbContext.SaveChangesAsync();
            return newFile;
        }
        catch
        {
            throw;
        }
    }
}
