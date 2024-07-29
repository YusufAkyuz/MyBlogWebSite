using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Contracts;

public interface IDashBoardService
{
    Task<List<int>> GetYearlyArticleCounts();
    Task<int> GetTotalArticleCount();
    Task<int> GetTotalCategoryCount();
}