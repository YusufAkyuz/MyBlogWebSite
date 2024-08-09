using Blog.Entity.DTOs.Comments;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Contracts;

public interface ICommentService
{
    Task<List<CommentDto>> GetAllACommentsWithArticleAsync();

    Task CreateCommentAsync(Guid articleId, string name, string email, string content);
    
    Task<List<CommentDto>> GetRecentCommentsAsync();
    Task<List<Comment>> GetCommentsWithArticleId(Guid articleId);
}