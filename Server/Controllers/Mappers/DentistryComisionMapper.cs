using CMVMD.Shared.Models;
using AutoMapper;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class DentistryComisionMapper : Profile
{
    public DentistryComisionMapper()
    {
        CreateMap<DentistryComisionMember, ExecutiveMemberDto>();
    }
}
