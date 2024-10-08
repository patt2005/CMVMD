using System.Net.Http.Json;
using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Article;

public class ArticleService : IArticleService
{
    private readonly HttpClient _httpClient;

    public ArticleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddArticleAsync(ArticleDto article)
    {
        await _httpClient.PostAsJsonAsync("api/articles/add", article);
    }

    public async Task DeleteById(string id)
    {
        await _httpClient.DeleteAsync($"api/articles/delete/{id}");
    }

    public async Task EditArticle(ArticleDto article)
    {
        await _httpClient.PutAsJsonAsync("api/articles/edit", article);
    }

    public Task<IEnumerable<ArticleDto>> GetAllArticlesAsync()
    {
        var httpResponse = _httpClient.GetFromJsonAsync<IEnumerable<ArticleDto>>("api/articles/getall");
        return httpResponse!;
    }

    public Task<ArticleDto> GetByIdAsync(string id)
    {
        var httpResponse = _httpClient.GetFromJsonAsync<ArticleDto>($"api/articles/{id}");
        return httpResponse!;
    }
}
