using CMVMD.Shared.Models;

namespace CMVMD.Client.Services;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetEventsAsync();
    Task AddEventAsync(EventDto eventDto);
    Task<EventDto> GetByIdAsync(string id);
}
