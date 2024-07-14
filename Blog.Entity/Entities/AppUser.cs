using Microsoft.AspNetCore.Identity;

namespace Blog.Entity.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Guid ImageId { get; set; } = Guid.Parse("F02F448C-6B36-4C2A-ADA7-7218CE5DBDC9");
    public Image Image { get; set; }
    public ICollection<Article> Articles { get; set; }
}