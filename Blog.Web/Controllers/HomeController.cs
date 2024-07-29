using System.Diagnostics;
using Blog.Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;

namespace Blog.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticleService _articlecleService;

    public HomeController(ILogger<HomeController> logger, IArticleService articlecleService)
    {
        _logger = logger;
        _articlecleService = articlecleService;
    }

    public async Task<IActionResult> Index(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
    {
        var articles = await _articlecleService.GetAllByPaginationAsync(categoryId, currentPage, pageSize, isAscending);
        return View(articles);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
}