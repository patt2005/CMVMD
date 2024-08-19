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
        var response = await _httpClient.GetAsync("api/training/getall");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("Got an error while retrieving data from api");
        }
        // var documents = await _httpClient.GetFromJsonAsync<IEnumerable<DocumentDto>>("api/training/getall");
        return Enumerable.Empty<DocumentDto>();
    }
}
