using AutoMapper;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Entities;

namespace CMVMD.Server.Controllers;

[ApiController]
[Route("api/training")]
public class TrainingController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public TrainingController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddTrainingDocument(DocumentDto documentJson)
    {
        var legislationDocument = _mapper.Map<TrainingDocument>(documentJson);
        _appDbContext.TrainingDocuments.Add(legislationDocument);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetTrainingDocuments()
    {
        var documents = await _appDbContext.TrainingDocuments.Include(d => d.File).ToListAsync();
        var jsonDocuments = _mapper.Map<IEnumerable<DocumentDto>>(documents);
        return Ok(jsonDocuments);
    }
}
