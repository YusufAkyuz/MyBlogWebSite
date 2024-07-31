namespace Blog.Entity.DTOs.Comments;

public class CommentDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Content { get; set; }
    public Guid ArticleId { get; set; }
}