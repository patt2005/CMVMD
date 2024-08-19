using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Document;

public interface IDocumentService
{
    Task AddLegislationDocument(DocumentDto document);
    Task AddTrainingDocument(DocumentDto document);
    Task<IEnumerable<DocumentDto>> GetLegislationDocuments();
    Task<IEnumerable<DocumentDto>> GetTrainingDocuments();
}
