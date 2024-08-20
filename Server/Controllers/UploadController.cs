using AutoMapper;
using CMVMD.Server.Services.File;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Persistance.Data;

namespace CMVMD.Server.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UploadController(IFileService fileService, AppDbContext appDbContext, IMapper mapper)
    {
        _fileService = fileService;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpPost("image")]
    public async Task<IActionResult> Image([FromForm] IFormFile file)
    {
        try
        {
            string fileName = await _fileService.Add(file);
            var url = Url.Content($"http://localhost:5243/Files/{fileName}");
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
                FileUrl = Url.Content($"http://localhost:5243/Files/{fileName}"),
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
