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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrainingDocumentById(string id)
    {
        var documentId = Guid.Parse(id);
        var document = await _appDbContext.TrainingDocuments.Include(d => d.File).FirstAsync(d => d.Id == documentId);
        var documentJson = _mapper.Map<DocumentDto>(document);
        return Ok(documentJson);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditTrainingDocument(DocumentDto documentJson)
    {
        var currentDocument = await _appDbContext.TrainingDocuments.FirstAsync(d => d.Id == documentJson.Id);

        if (currentDocument == null)
        {
            return NotFound();
        }

        _mapper.Map(documentJson, currentDocument);

        _appDbContext.TrainingDocuments.Update(currentDocument);
        await _appDbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var guidId = Guid.Parse(id);
        var document = await _appDbContext.TrainingDocuments.FirstAsync(d => d.Id == guidId);

        if (document == null)
        {
            return NotFound();
        }
        _appDbContext.TrainingDocuments.Remove(document);

        await _appDbContext.SaveChangesAsync();
        return Ok();
    }
}
