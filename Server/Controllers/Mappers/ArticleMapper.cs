using AutoMapper;
using CMVMD.Shared.Models;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class ArticleMapper : Profile
{
    public ArticleMapper()
    {
        CreateMap<Article, ArticleDto>().ReverseMap();
    }
}
