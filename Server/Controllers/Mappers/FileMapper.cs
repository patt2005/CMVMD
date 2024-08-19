using AutoMapper;
using CMVMD.Shared.Models;

namespace CMVMD.Server.Controllers.Mappers;

public class FileMapper : Profile
{
    public FileMapper()
    {
        CreateMap<Persistance.Entities.File, FileDto>()
        .ReverseMap()
        .ForMember(x => x.Id, opt => opt.Ignore())
        ;
    }
}