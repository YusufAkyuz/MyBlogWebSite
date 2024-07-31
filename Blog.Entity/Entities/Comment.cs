using Blog.Core.Entities;

namespace Blog.Entity.Entities;

public class Comment : IEntityBase
{
    public Comment()
    {
        
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public Guid ArticleId { get; set; }
}