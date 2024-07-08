using Microsoft.AspNetCore.Identity;

namespace Blog.Entity.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}