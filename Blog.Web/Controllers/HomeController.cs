using System.Diagnostics;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Comments;
using Blog.Entity.Entities;
using Blog.Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using NToastNotify;

namespace Blog.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticleService _articlecleService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentService _commentService;
    private readonly IToastNotification _toastNotification;

    public HomeController(ILogger<HomeController> logger, IArticleService articlecleService, 
        IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, ICommentService commentService
        ,IToastNotification toastNotification)
    {
        _logger = logger;
        _articlecleService = articlecleService;
        _contextAccessor = contextAccessor;
        _unitOfWork = unitOfWork;
        _commentService = commentService;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
    {
        var articles = await _articlecleService.GetAllByPaginationAsync(categoryId, currentPage, pageSize, isAscending);
        return View(articles);
    }
    [HttpGet]
    public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
    {
        var articles = await _articlecleService.SearchAsync(keyword, currentPage, pageSize, isAscending);
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

    public async Task<IActionResult> ArticleDetail(Guid articleId)
    {
        var ipAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        
        var articeVisitors = await _unitOfWork.GetRepository<ArticleVisitor>().GetAllAsync(null, x => x.Visitor, y => y.Article);
        var article = await _unitOfWork.GetRepository<Article>().GetAsync(x => x.Id == articleId);

        var result = await _articlecleService.GetArticleWithCategoryNonDeletedAsync(articleId);

        var visitor = await _unitOfWork.GetRepository<Visitor>().GetAsync(x => x.IpAddress == ipAddress);

        var addArticleVisitors = new ArticleVisitor(article.Id,visitor.Id);

        if (articeVisitors.Any(x =>
                x.VisitorId == addArticleVisitors.VisitorId && x.ArticleId == addArticleVisitors.ArticleId))
        {
            result.Comments = await _commentService.GetCommentsWithArticleId(articleId);
            return View(result);
        }
        else
        {
            await _unitOfWork.GetRepository<ArticleVisitor>().AddAsync(addArticleVisitors);
            article.ViewCount += 1;
            await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            result.ViewCount++;
        }

        result.Comments = await _commentService.GetCommentsWithArticleId(articleId);
        return View(result);
    }

    

    
}