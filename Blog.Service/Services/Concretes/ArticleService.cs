using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace Blog.Service.Services.Concretes;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _accessor;

    public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _accessor = accessor;
    }
    public async Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync()
    {
        var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(
            x => !x.IsDeleted, x=>x.Category);
        var map = _mapper.Map<List<ArticleDto>>(articles);
        return map;
    }

    public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
    {
        var userId = _accessor.HttpContext.User.GetLoggedInUserId();
        var userEmail = _accessor.HttpContext.User.GetLoggedInUserEmail();
        
        var imageId = Guid.Parse("F02F448C-6B36-4C2A-ADA7-7218CE5DBDC9");
        
        var article = new Article(title : articleAddDto.Title, content: articleAddDto.Content,
            userId : userId, createdBy:userEmail,
            categoryId:articleAddDto.CategoryId, imageId:imageId);
        
        await _unitOfWork.GetRepository<Article>().AddAsync(article);
        await _unitOfWork.SaveAsync();
    }

    public async Task<ArticleDto> GetArticle(Guid id)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetAsync(
            x => !x.IsDeleted && x.Id == id, x => x.Category
        );
        var map = _mapper.Map<ArticleDto>(article);
        return map;
    }

    public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
    {
        var article = await _unitOfWork.GetRepository<Article>()
            .GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category);
        // TODO: ilerleyen süreçte mapper ile güncelleme ekliyeceğiz 
        article.Title = articleUpdateDto.Title; 
        article.Content = articleUpdateDto.Content;
        article.CategoryId = articleUpdateDto.CategoryId;
        article.ModifiedDate = DateTime.UtcNow;
        article.ModifiedBy = _accessor.HttpContext.User.GetLoggedInUserEmail();
        
        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();
        return article.Title;
    }

    public async Task<string> SafeDeleteArticleAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
        article.IsDeleted = true;
        article.DeletedDate = DateTime.UtcNow;
        article.DeletedBy = _accessor.HttpContext.User.GetLoggedInUserEmail();
        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();
        return article.Title;
    }
}   