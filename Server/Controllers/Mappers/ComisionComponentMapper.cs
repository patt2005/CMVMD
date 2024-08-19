using CMVMD.Shared.Models;
using AutoMapper;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class ComisionComponentMapper : Profile
{
    public ComisionComponentMapper()
    {
        CreateMap<ComisionComponentMember, ExecutiveMemberDto>();
    }
}
