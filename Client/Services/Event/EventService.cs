using System.Net.Http.Json;
using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Event;

public class EventService : IEventService
{
    private readonly HttpClient _httpClient;

    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddEventAsync(EventDto eventDto)
    {
        await _httpClient.PostAsJsonAsync("api/event/add", eventDto);
    }

    public async Task<EventDto> GetByIdAsync(string id)
    {
        var eventJson = await _httpClient.GetFromJsonAsync<EventDto>($"api/event/{id}");
        return eventJson!;
    }

    public async Task<IEnumerable<EventDto>> GetEventsAsync()
    {
        var events = await _httpClient.GetFromJsonAsync<IEnumerable<EventDto>>("api/event/getall");
        return events!;
    }
}
