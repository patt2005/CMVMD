using CMVMD.Shared.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CMVMD.Client.Services.Article;

public interface IArticleService
{
    Task<IEnumerable<ArticleDto>> GetAllArticlesAsync();
    Task AddArticleAsync(ArticleDto article);
    Task<ArticleDto> GetByIdAsync(string id);
}
