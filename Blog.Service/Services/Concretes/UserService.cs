using System.Security.Claims;
using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Users;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Blog.Service.Extensions;
using Blog.Service.FluentValidations;
using Blog.Service.Helpers.Images;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Blog.Service.Services.Concretes;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserValidator _userValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageHelper _imageHelper;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IToastNotification _toastNotification;
    private readonly ClaimsPrincipal _claimsPrincipal;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, 
        RoleManager<AppRole> roleManager, UserValidator userValidator, IHttpContextAccessor httpContextAccessor,
        IImageHelper imageHelper, SignInManager<AppUser> signInManager,  IToastNotification toastNotification)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _userValidator = userValidator;
        _httpContextAccessor = httpContextAccessor;
        _imageHelper = imageHelper;
        _signInManager = signInManager;
        _toastNotification = toastNotification;
        _claimsPrincipal = _httpContextAccessor.HttpContext.User;
    }
    public async Task<List<UserDto>> GetAllUsersWithRoleAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var map = _mapper.Map<List<UserDto>>(users);

        foreach (var item in map)
        {
            var findUser = await _userManager.FindByIdAsync(item.Id.ToString());
            //bir kişinin birden fazla rolü varsa superadmin admin user gibi bu string.Join("-",listeAdı) ilk parametreye göre ayırır ve
            //ona göre bir string oluşturur burda superadmin-admin-user gibi bir string oluşacaktır. Bizim projede her kullanıcı bir role sahp old için çok önemli değil bu yapı şu anlık
            var role = string.Join("", await _userManager.GetRolesAsync(findUser));
            item.Role = role;                       //--> identity yapısında gelen AppUser role bölümüne sahip değil bu yüzden manuel olarak tanımlıyoruz

        }

        return map;
    }

    public async Task<List<AppRole>> GetAllRolesAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<ValidationResult> Validate(UserAddDto userAddDto)
    {
        var map = _mapper.Map<AppUser>(userAddDto);
        var validation = await _userValidator.ValidateAsync(map);
        return validation;
    }

    public async Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto)
    {
        var map = _mapper.Map<AppUser>(userAddDto);
        map.UserName = userAddDto.Email;
        var result = await _userManager.CreateAsync(map, string.IsNullOrEmpty(userAddDto.Password) ? "" : userAddDto.Password);
        if (result.Succeeded)
        {
            var findRole = await _roleManager.FindByIdAsync(userAddDto.RoleId.ToString());
            await _userManager.AddToRoleAsync(map, findRole.ToString());
        }

        return result;
    }

    public async Task<AppUser> GetUserByIdAsync(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
    {
        var user = await GetUserByIdAsync(userUpdateDto.Id);
        var userRole = await GetUserRoleAsync(user);
        
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            await _userManager.RemoveFromRoleAsync(user, userRole);
            var findNewRole = await _roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
            await _userManager.AddToRoleAsync(user, findNewRole.Name);
        }

        return result;
    }

    public async Task<string> GetUserRoleAsync(AppUser user)
    {
        return string.Join("", await _userManager.GetRolesAsync(user));
    }

    public async Task<(IdentityResult identityResult, string? userEmail)> DeleteUserAsync(Guid userId)
    {
        var user = await GetUserByIdAsync(userId);
        return (await _userManager.DeleteAsync(user), user.Email);
    }
    
    public async Task<UserProfileDto> GetUserProfileDtoAsync()
    {
        var userId = _claimsPrincipal.GetLoggedInUserId();
        var getUserWithImage = await _unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == userId,
            x => x.Image);
        var map = _mapper.Map<UserProfileDto>(getUserWithImage);
        map.Image.FileName = getUserWithImage.Image.FileName;
        return map;
    }
    
    private async Task<Guid> UploadImage(UserProfileDto userProfileDto)
        {
            var userEmail = _claimsPrincipal.GetLoggedInUserEmail();

            var imageUpload = await _imageHelper.Upload($"{userProfileDto.FirstName}{userProfileDto.LastName}", userProfileDto.Photo, ImageType.User);
            Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, userEmail);
            await _unitOfWork.GetRepository<Image>().AddAsync(image);

            return image.Id;
        }

        public async Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto)
        {
            var userId = _claimsPrincipal.GetLoggedInUserId();
            var user = await GetUserByIdAsync(userId);
            var oldImageId = user.ImageId;
            
            if (userProfileDto.CurrentPassword is null)
            {
                _toastNotification.AddErrorToastMessage("Lütfen Mevcut Şifrenizi Girin.");
                return false;
            }


            var isVerified = await _userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);
            if (isVerified && userProfileDto.NewPassword != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);

                    _mapper.Map(userProfileDto, user);

                    if(userProfileDto.Photo != null)
                        user.ImageId = await UploadImage(userProfileDto);

                    await _userManager.UpdateAsync(user);
                    await _unitOfWork.SaveAsync();

                    return true;
                }
                else
                    return false;
            }
            else if (isVerified)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                _mapper.Map(userProfileDto, user);

                if (userProfileDto.Photo != null)
                        user.ImageId = await UploadImage(userProfileDto);
                
                await _userManager.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
                return false;
        }
}