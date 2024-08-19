using AutoMapper;
using CMVMD.Shared.Models;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class EventMapper : Profile
{
    public EventMapper()
    {
        CreateMap<Event, EventDto>().ReverseMap();
    }
}
