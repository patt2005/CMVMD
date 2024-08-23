using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Event;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetEventsAsync();
    Task AddEventAsync(EventDto eventDto);
    Task<EventDto> GetByIdAsync(string id);
    Task DeleteById(string id);
    Task EditEvent(EventDto article);
}
