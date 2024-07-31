using Blog.Data.UnitOfWorks;
using Microsoft.AspNetCore.Mvc.Filters;
using Blog.Entity.Entities;

namespace Blog.Web.Filters.ArticleVisitors;

public class ArticleVisitorFilter : IAsyncActionFilter
{
    private readonly IUnitOfWork _unitOfWork;

    public ArticleVisitorFilter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        List<Visitor> visitors = _unitOfWork.GetRepository<Visitor>().GetAllAsync().Result;
            
        string getIp = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        string getUserAgent = context.HttpContext.Request.Headers["User-Agent"];
        
        Visitor visitor = new ( getIp, getUserAgent );

        if (visitors.Any(x => x.IpAddress == visitor.IpAddress))
        {
            return next();
        }
        else
        {
            Task.FromResult(_unitOfWork.GetRepository<Visitor>().AddAsync(visitor));
            Task.FromResult(_unitOfWork.SaveAsync());
            return next();
        }
    }
}