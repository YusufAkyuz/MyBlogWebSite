using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;
using Blog.Service.Services.Contracts;

namespace Blog.Service.Services.Concretes;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
        var userId = Guid.Parse("C1E1C41C-2F28-436A-9FC7-3EAA4567C792");
        var imageId = Guid.Parse("F02F448C-6B36-4C2A-ADA7-7218CE5DBDC9");
        var article = new Article(title : articleAddDto.Title, content: articleAddDto.Content, userId : userId,
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

    public async Task UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
    {
        var article = await _unitOfWork.GetRepository<Article>()
            .GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category);
        // TODO: ilerleyen süreçte mapper ile güncelleme ekliyeceğiz 
        article.Title = articleUpdateDto.Title; 
        article.Content = articleUpdateDto.Content;
        article.CategoryId = articleUpdateDto.CategoryId;
        
        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();
    }

    public async Task SafeDeleteArticleAsync(Guid articleId)
    {
        var article = await _unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
        article.IsDeleted = true;
        article.DeletedDate = DateTime.UtcNow;
        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();
    }
}   