using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Contracts;

namespace Blog.Service.Services.Concretes;

public class DashboardService : IDashBoardService
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<int>> GetYearlyArticleCounts()
    {
        var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted);

        var startDate = DateTime.Now.Date;
        startDate = new DateTime(startDate.Year, 1, 1);

        List<int> datas = new();

        for (int i = 1; i <= 12; i++)
        {
            var startedDate = new DateTime(startDate.Year, i, 1);
            var endedDate = startedDate.AddMonths(1);
            var data = articles.Where(x => x.CreatedDate >= startedDate && x.CreatedDate < endedDate).Count();
            datas.Add(data);
        }

        return datas;
    }
    public async Task<int> GetTotalArticleCount()
    {
        var articleCount = await _unitOfWork.GetRepository<Article>().CountAsync();
        return articleCount;
    }
    public async Task<int> GetTotalCategoryCount()
    {
        var categoryCount = await _unitOfWork.GetRepository<Category>().CountAsync();
        return categoryCount;
    }
}