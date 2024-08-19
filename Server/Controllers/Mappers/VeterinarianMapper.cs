using AutoMapper;
using CMVMD.Shared.Models;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class VeterinarianMapper : Profile
{
    public VeterinarianMapper()
    {
        CreateMap<Veterinarian, VeterinarianDto>().ReverseMap();
    }
}
