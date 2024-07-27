using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Entity.Entities;

public class AppUser : IdentityUser<Guid>, IEntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Guid ImageId { get; set; } = Guid.Parse("58c781bf-d6fd-4cec-9da5-5ab5e6b70eee");
    public Image Image { get; set; }
    public ICollection<Article> Articles { get; set; }
}