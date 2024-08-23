using CMVMD.Shared.Models;

namespace CMVMD.Client.Services.Article;

public interface IArticleService
{
    Task<IEnumerable<ArticleDto>> GetAllArticlesAsync();
    Task AddArticleAsync(ArticleDto article);
    Task<ArticleDto> GetByIdAsync(string id);
    Task DeleteById(string id);
    Task EditArticle(ArticleDto article);
}
