using Blog.Core.Entities;

namespace Blog.Entity.Entities;

public class Visitor : IEntityBase
{
    public Visitor()
    {
        
    }

    public Visitor(string ıpAddress, string userAgent)
    {
        IpAddress = ıpAddress;
        UserAgent = userAgent;
    }

    public int Id { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }   //Kullanıcıların sisteme hangi tarayıcıdan girdiğini anlamak için kullanacaz
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public ICollection<ArticleVisitors> ArticleVisitors { get; set; }
}