using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Contracts;

namespace Blog.Service.Services.Concretes;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;

    public ArticleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<Article>> GetAllArticleAsync()
    {
        return await _unitOfWork.GetRepository<Article>().GetAllAsync();
    }
}   