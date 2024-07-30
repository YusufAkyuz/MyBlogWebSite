using Blog.Core.Entities;

namespace Blog.Entity.Entities;

public class ArticleVisitors : IEntityBase
{
    //Many to many ilişkileri kurmak için oluşturduk bu tabloyu
    public ArticleVisitors(Guid articleId, int visitorId)
    {
        ArticleId = articleId;
        VisitorId = visitorId;
    }
    public Guid ArticleId { get; set; }
    public Article Article { get; set; }
    public int VisitorId { get; set; }
    public Visitor Visitor { get; set; }
}