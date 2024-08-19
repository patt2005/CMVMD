using AutoMapper;
using CMVMD.Shared.Models;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class LegislationDocumentMapper : Profile
{
    public LegislationDocumentMapper()
    {
        CreateMap<LegislationDocument, DocumentDto>().ReverseMap();
    }
}