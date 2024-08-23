using CMVMD.Shared.Models;
using AutoMapper;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class ExecutiveOfficeMapper : Profile
{
    public ExecutiveOfficeMapper()
    {
        CreateMap<ExecutiveOfficeMember, ExecutiveMemberDto>().ReverseMap();
    }
}
