using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Contracts;

public interface IArticleService
{
    Task<List<ArticleDto>> GetAllArticleAsync();
}