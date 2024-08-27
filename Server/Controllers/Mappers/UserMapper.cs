using AutoMapper;
using CMVMD.Shared.Models;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserDto, User>().ReverseMap();
    }
}
