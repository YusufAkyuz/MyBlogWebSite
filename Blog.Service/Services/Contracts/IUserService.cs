using Blog.Entity.DTOs.Users;
using Blog.Entity.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Blog.Service.Services.Concretes;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersWithRoleAsync();
    Task<List<AppRole>> GetAllRolesAsync();
    Task<ValidationResult> Validate(UserAddDto userAddDto);
    Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto);
    Task<AppUser> GetUserByIdAsync(Guid userId);
    Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto);
    Task<string> GetUserRoleAsync(AppUser user);
    Task<(IdentityResult identityResult, string? userEmail)> DeleteUserAsync(Guid userId);
    Task<UserProfileDto> GetUserProfileDtoAsync();
    Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto);
}