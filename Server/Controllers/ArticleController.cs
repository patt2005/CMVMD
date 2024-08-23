using AutoMapper;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Entities;

namespace CMVMD.Server.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public ArticleController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllArticles()
    {
        var articles = await _appDbContext.Articles.Include(a => a.File).ToListAsync();
        var response = _mapper.Map<IEnumerable<ArticleDto>>(articles);
        return Ok(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteArticle(string id)
    {
        var articleId = Guid.Parse(id);
        var article = await _appDbContext.Articles.FirstAsync(a => a.Id == articleId);

        if (article == null)
        {
            return NotFound();
        }

        _appDbContext.Articles.Remove(article);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddArticle(ArticleDto articleDto)
    {
        var article = _mapper.Map<Article>(articleDto);

        _appDbContext.Articles.Add(article);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArticleById(string id)
    {
        var guidId = Guid.Parse(id);
        var article = await _appDbContext.Articles.Include(a => a.File).FirstAsync(a => a.Id == guidId);
        var data = _mapper.Map<ArticleDto>(article);
        return Ok(data);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditArticle(ArticleDto articleJson)
    {
        var currentArticle = await _appDbContext.Articles.FirstAsync(a => a.Id == articleJson.Id);

        _mapper.Map(articleJson, currentArticle);

        _appDbContext.Articles.Update(currentArticle);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}
