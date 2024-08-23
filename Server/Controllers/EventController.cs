using AutoMapper;
using CMVMD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Entities;

namespace CMVMD.Server.Controllers;

[ApiController]
[Route("api/event")]
public class EventController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public EventController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddEvent(EventDto eventDto)
    {
        var eventObj = _mapper.Map<Event>(eventDto);
        _appDbContext.Events.Add(eventObj);

        await _appDbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(string id)
    {
        Guid eventId = Guid.Parse(id);
        var eventObj = await _appDbContext.Events.Include(e => e.File).FirstAsync(e => e.Id == eventId);
        var jsonEvent = _mapper.Map<EventDto>(eventObj);
        return Ok(jsonEvent);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        var articleId = Guid.Parse(id);
        var eventObj = await _appDbContext.Events.FirstAsync(a => a.Id == articleId);

        if (eventObj == null)
        {
            return NotFound();
        }

        _appDbContext.Events.Remove(eventObj);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _appDbContext.Events.Include(e => e.File).OrderBy(e => e.StartDate).ToListAsync();
        var eventsJson = _mapper.Map<IEnumerable<EventDto>>(events);
        return Ok(eventsJson);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditEvent(EventDto eventJson)
    {
        var currentEvent = await _appDbContext.Events.FirstAsync(a => a.Id == eventJson.Id);

        _mapper.Map(eventJson, currentEvent);

        _appDbContext.Events.Update(currentEvent);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}
