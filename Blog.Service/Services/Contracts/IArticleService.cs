using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Contracts;

public interface IArticleService
{
    Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync();
    Task<List<ArticleDto>> GetAllArticlesWithCategoryDeletedAsync();

    Task CreateArticleAsync(ArticleAddDto articleAddDto);
    Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId);

    Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);
    Task<string> SafeDeleteArticleAsync(Guid articleId);
    Task<string> UndoDeleteArticleAsync(Guid articleId);

    Task<ArticleListDto> GetAllByPaginationAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3,
        bool isAscending = false);
    Task<ArticleListDto> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3,
        bool isAscending = false);
    
    //Recent Articles
    Task<List<ArticleDto>> GetRecentArticlesAsyncNonDeleted();
}