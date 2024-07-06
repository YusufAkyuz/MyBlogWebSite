using Blog.Core.Entities;

namespace Blog.Entity.Entities;

public class Image : EntityBase , IEntityBase
{
    public string FileName { get; set; }
    public string FileType { get; set; }
    // Aşağıdaki prop'u One To Many ilişkisini kurmak adına ekledik. Bu image birden fazla articleda olabilir
    public ICollection<Article> Articles { get; set; }
}