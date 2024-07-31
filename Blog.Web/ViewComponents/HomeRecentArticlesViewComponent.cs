using Blog.Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents;

public class HomeRecentArticlesViewComponent : ViewComponent
{
    private readonly IArticleService _articleService;

    public HomeRecentArticlesViewComponent(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var recentArticles = await _articleService.GetRecentArticlesAsyncNonDeleted();
        return View(recentArticles);
    }
    
}