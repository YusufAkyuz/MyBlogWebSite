using Blog.Entity.Entities;
using Microsoft.AspNetCore.Http;

namespace Blog.Entity.DTOs.Users;

public class UserProfileDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }

    public IFormFile? Photo { get; set; }
    public Image Image { get; set; }
}