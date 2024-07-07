using Blog.Entity.Entities;

namespace Blog.Service.Services.Contracts;

public interface IArticleService
{
    Task<List<Article>> GetAllArticleAsync();
}