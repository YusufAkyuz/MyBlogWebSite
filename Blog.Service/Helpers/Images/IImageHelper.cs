using Blog.Entity.DTOs.Images;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Http;

namespace Blog.Service.Helpers.Images;

public interface IImageHelper
{
    Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null);
    void Delete(string imageName);
}