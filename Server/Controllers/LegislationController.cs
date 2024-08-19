using AutoMapper;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Entities;

namespace CMVMD.Server.Controllers;

[ApiController]
[Route("api/legislation")]
public class LegislationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public LegislationController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddDocument(DocumentDto documentJson)
    {
        var legislationDocument = _mapper.Map<LegislationDocument>(documentJson);
        _appDbContext.LegislationDocuments.Add(legislationDocument);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetDocuments()
    {
        var documents = await _appDbContext.LegislationDocuments.Include(d => d.File).ToListAsync();
        var jsonDocuments = _mapper.Map<IEnumerable<DocumentDto>>(documents);
        return Ok(jsonDocuments);
    }
}
