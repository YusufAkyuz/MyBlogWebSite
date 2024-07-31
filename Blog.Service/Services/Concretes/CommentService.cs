using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Comments;
using Blog.Entity.Entities;
using Blog.Service.Services.Contracts;

namespace Blog.Service.Services.Concretes;

public class CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<List<CommentDto>> GetAllACommentsWithArticleAsync()
    {
        var comments = await _unitOfWork.GetRepository<Comment>().GetAllAsync();
        var mapComments = _mapper.Map<List<CommentDto>>(comments);
        return mapComments;
    }

    public async Task CreateCommentAsync(Guid articleId, string name, string email, string content)
    {
        var commentAddDto = new CommentAddDto() { Name = name, Content = content, Email = email };
        var map = _mapper.Map<Comment>(commentAddDto);
        map.ArticleId = articleId;
        await _unitOfWork.GetRepository<Comment>().AddAsync(map);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<CommentDto>> GetRecentCommentsAsync()
    {
        var comments = await _unitOfWork.GetRepository<Comment>().GetAllAsync();
        var mapCommentDto = _mapper.Map<List<CommentDto>>(comments);
        return mapCommentDto;
    }
}