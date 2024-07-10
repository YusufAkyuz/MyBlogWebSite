using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Contracts;

public interface IArticleService
{
    Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync();

    Task CreateArticleAsync(ArticleAddDto articleAddDto);
    Task<ArticleDto> GetArticle(Guid id);

    Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);
    Task<string> SafeDeleteArticleAsync(Guid articleId);
}