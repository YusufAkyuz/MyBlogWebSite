using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Service.Services.Concretes;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _accessor;
    private readonly IImageHelper _imageHelper;

    public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accessor, IImageHelper imageHelper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _accessor = accessor;
        _imageHelper = imageHelper;
    }
    public async Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync()
    {
        var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(
            x => !x.IsDeleted, x=>x.Category);
        var map = _mapper.Map<List<ArticleDto>>(articles);
        return map;
    }

    public async Task<List<ArticleDto>> GetAllArticlesWithCategoryDeletedAsync()
    {
        var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(
            x => x.IsDeleted, x=>x.Category);
        var map = _mapper.Map<List<ArticleDto>>(articles);
        return map;
    }

    public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
    {
        var userId = _accessor.HttpContext.User.GetLoggedInUserId();
        var userEmail = _accessor.HttpContext.User.GetLoggedInUserEmail();

        var imageUploadDto = await _imageHelper.Upload(articleAddDto.Title, articleAddDto.Photo, ImageType.Post);
        
        Image image = new Image(imageUploadDto.FullName, articleAddDto.Photo.ContentType, userEmail);
        
        await _unitOfWork.GetRepository<Image>().AddAsync(image);
        
        var article = new Article(title : articleAddDto.Title, content: articleAddDto.Content,
            userId : userId, createdBy:userEmail,
            categoryId:articleAddDto.CategoryId, imageId:image.Id);
        
        await _unitOfWork.GetRepository<Article>().AddAsync(article);
        await _unitOfWork.SaveAsync();
    }

    public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
    {

        var article = await _unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category, x => x.Image);
        var map = _mapper.Map<ArticleDto>(article);

        return map;
    }

    public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
    {
        var userEmail = _accessor.HttpContext.User.GetLoggedInUserEmail();
        var article = await _unitOfWork.GetRepository<Article>()
            .GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category, i => i.Image);
        // TODO: ilerleyen süreçte mapper ile güncelleme ekliyeceğiz 

        if (articleUpdateDto.Photo != null)
        {
            // Delete the old image
            if (article.Image != null)
            {
                _imageHelper.Delete(article.Image.FileName);
            }

            // Upload new image
            var imageUpload = await _imageHelper.Upload(articleUpdateDto.Title, articleUpdateDto.Photo, ImageType.Post);
            Image image = new Image(imageUpload.FullName, articleUpdateDto.Photo.ContentType, userEmail);
            await _unitOfWork.GetRepository<Image>().AddAsync(image);

            article.ImageId = image.Id;
        }

        _mapper.Map(articleUpdateDto, article);
        // article.Title = articleUpdateDto.Title;          |
        // article.Content = articleUpdateDto.Content;      |   Bu üç satırı maping sayesinde kısalttık
        // article.CategoryId = articleUpdateDto.CategoryId;|
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

    public async Task<string> UndoDeleteArticleAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
        article.IsDeleted = false;
        
        article.DeletedDate = null;
        article.DeletedBy = null;
        
        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();
        
        return article.Title;
    }
    
    //Pagination Process
    public async Task<ArticleListDto> GetAllByPaginationAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3,
        bool isAscending = false)
    {
        pageSize = pageSize > 20 ? 20 : pageSize;
        var articles = categoryId == null
            ? await _unitOfWork.GetRepository<Article>().GetAllAsync(a => !a.IsDeleted,
                a => a.Category, a => a.Image, u=>u.User)
            : await _unitOfWork.GetRepository<Article>().GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted,
                a => a.Category, a => a.Image, u=>u.User);

        var sortedArticles = isAscending
            ? articles.OrderBy(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
            : articles.OrderByDescending(x => x.CreatedDate).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        return new ArticleListDto()
        {
            Articles = sortedArticles,
            CategoryId = categoryId == null ? null : categoryId.Value,
            PageSize = pageSize,
            CurrentPage = currentPage,
            IsAscending = isAscending,
            TotalCount = articles.Count
        };
    }
}   