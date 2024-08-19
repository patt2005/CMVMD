using AutoMapper;
using CMVMD.Shared.Models;
using Persistance.Entities;

namespace CMVMD.Server.Controllers.Mappers;

public class TrainingDocumentMapper : Profile
{
    public TrainingDocumentMapper()
    {
        CreateMap<TrainingDocument, DocumentDto>().ReverseMap();
    }
}
