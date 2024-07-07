using Blog.Core.Entities;

namespace Blog.Entity.Entities;

public class Category : EntityBase
{
    public string Name { get; set; }
    // Aşağıdaki prop'u One To Many ilişkisini kurmak adına ekledik. Bu image birden fazla articleda olabilir
    public ICollection<Article> Articles { get; set; } 
}