using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Document;

public interface IDocumentService
{
    Task AddLegislationDocument(DocumentDto document);
    Task AddTrainingDocument(DocumentDto document);
    Task<IEnumerable<DocumentDto>> GetLegislationDocuments();
    Task<IEnumerable<DocumentDto>> GetTrainingDocuments();
    Task<DocumentDto> GetLegislationDocumentById(string id);
    Task EditLegislationDocument(DocumentDto document);
    Task<DocumentDto> GetTrainingDocumentById(string id);
    Task EditTrainingDocument(DocumentDto document);
    Task DeleteLegislationDocumentById(string id);
    Task DeleteTrainingDocumentById(string id);
}
