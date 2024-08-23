using System.Net.Http.Json;
using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Document;

public class DocumentService : IDocumentService
{
    private readonly HttpClient _httpClient;

    public DocumentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<DocumentDto>> GetLegislationDocuments()
    {
        var documents = await _httpClient.GetFromJsonAsync<IEnumerable<DocumentDto>>("api/legislation/getall");
        return documents!;
    }

    public async Task AddLegislationDocument(DocumentDto document)
    {
        await _httpClient.PostAsJsonAsync("api/legislation/add", document);
    }

    public async Task AddTrainingDocument(DocumentDto document)
    {
        await _httpClient.PostAsJsonAsync("api/training/add", document);
    }

    public async Task<IEnumerable<DocumentDto>> GetTrainingDocuments()
    {
        var documents = await _httpClient.GetFromJsonAsync<IEnumerable<DocumentDto>>("api/training/getall");
        return documents!;
    }

    public async Task<DocumentDto> GetLegislationDocumentById(string id)
    {
        var documentDto = await _httpClient.GetFromJsonAsync<DocumentDto>($"api/legislation/{id}");
        return documentDto!;
    }

    public async Task EditLegislationDocument(DocumentDto documentDto)
    {
        await _httpClient.PutAsJsonAsync("api/legislation/edit", documentDto);
    }

    public async Task<DocumentDto> GetTrainingDocumentById(string id)
    {
        var documentDto = await _httpClient.GetFromJsonAsync<DocumentDto>($"api/training/{id}");
        return documentDto!;
    }

    public async Task EditTrainingDocument(DocumentDto document)
    {
        await _httpClient.PutAsJsonAsync("api/training/edit", document);
    }

    public async Task DeleteLegislationDocumentById(string id)
    {
        await _httpClient.DeleteAsync($"api/legislation/delete/{id}");
    }

    public async Task DeleteTrainingDocumentById(string id)
    {
        await _httpClient.DeleteAsync($"api/training/delete/{id}");
    }
}
