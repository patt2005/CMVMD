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
    public async Task<IActionResult> AddLegislationDocument(DocumentDto documentJson)
    {
        var legislationDocument = _mapper.Map<LegislationDocument>(documentJson);
        _appDbContext.LegislationDocuments.Add(legislationDocument);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetLegislationDocuments()
    {
        var documents = await _appDbContext.LegislationDocuments.Include(d => d.File).ToListAsync();
        var jsonDocuments = _mapper.Map<IEnumerable<DocumentDto>>(documents);
        return Ok(jsonDocuments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLegislationDocumentById(string id)
    {
        var documentId = Guid.Parse(id);
        var document = await _appDbContext.LegislationDocuments.Include(d => d.File).FirstAsync(d => d.Id == documentId);
        var documentJson = _mapper.Map<DocumentDto>(document);
        return Ok(documentJson);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditLegislationDocument(DocumentDto documentJson)
    {
        var currentDocument = await _appDbContext.LegislationDocuments.FirstAsync(d => d.Id == documentJson.Id);

        if (currentDocument == null)
        {
            return NotFound();
        }

        _mapper.Map(documentJson, currentDocument);

        _appDbContext.LegislationDocuments.Update(currentDocument);
        await _appDbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var guidId = Guid.Parse(id);
        var document = await _appDbContext.LegislationDocuments.FirstAsync(d => d.Id == guidId);

        if (document == null)
        {
            return NotFound();
        }
        _appDbContext.LegislationDocuments.Remove(document);

        await _appDbContext.SaveChangesAsync();
        return Ok();
    }
}
