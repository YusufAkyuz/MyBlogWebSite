using Blog.Entity.DTOs.Images;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Blog.Service.Helpers.Images;

public class ImageHelper : IImageHelper
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string wwwroot;
    private const string imageFolder = "images";
    private const string articleImagesFolder = "article-images";
    private const string userImagesFolder = "user-images";

    public ImageHelper(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        wwwroot = _webHostEnvironment.WebRootPath;
    }

    private string ReplaceInvalidChars(string fileName)
    {
        return fileName.Replace("İ", "I")
            .Replace("ı", "i")
            .Replace("Ğ", "G")
            .Replace("ğ", "g")
            .Replace("Ü", "U")
            .Replace("ü", "u")
            .Replace("ş", "s")
            .Replace("Ş", "S")
            .Replace("Ö", "O")
            .Replace("ö", "o")
            .Replace("Ç", "C")
            .Replace("ç", "c")
            .Replace("é", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("^", "")
            .Replace("+", "")
            .Replace("%", "")
            .Replace("/", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("=", "")
            .Replace("?", "")
            .Replace("_", "")
            .Replace("*", "")
            .Replace("æ", "")
            .Replace("ß", "")
            .Replace("@", "")
            .Replace("€", "")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("#", "")
            .Replace("$", "")
            .Replace("½", "")
            .Replace("{", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("}", "")
            .Replace(@"\", "")
            .Replace("|", "")
            .Replace("~", "")
            .Replace("¨", "")
            .Replace(",", "")
            .Replace(";", "")
            .Replace("`", "")
            .Replace(".", "")
            .Replace(":", "")
            .Replace(" ", "");
    }
    
    
    public async Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null)
    {
        folderName = imageType == ImageType.User ? userImagesFolder : articleImagesFolder;
        
        if (!Directory.Exists($"{wwwroot}/{imageFolder}/{folderName}"))
        {
            Directory.CreateDirectory($"{wwwroot}/{imageFolder}/{folderName}");
        }

        string oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
        string fileExtension = Path.GetExtension(imageFile.FileName);
        name = ReplaceInvalidChars(name);
        DateTime dateTime = DateTime.UtcNow;
        string newFileName = $"{name}_{dateTime.Millisecond}{fileExtension}";
        var path = Path.Combine($"{wwwroot}/{imageFolder}/{folderName}", newFileName);

        await using var stream = new FileStream(path, FileMode.Create,FileAccess.Write, FileShare.None, 1024*1024, useAsync:false);
        await imageFile.CopyToAsync(stream);
        await stream.FlushAsync();

        string message = imageType == ImageType.User
            ? $"{newFileName} isimli kullanıcı resmi başarıyla eklendi"
            : $"{newFileName} isimli makale resmi başarı ile eklendi";

        return new ImageUploadedDto()
        {
            FullName = $"{folderName}/{newFileName}"
        };
    }

    public void Delete(string imageName)
    {
        var fileToDelete = Path.Combine($"{wwwroot}/{imageFolder}/{imageName}");
        if (File.Exists(fileToDelete))
        {
            File.Delete(fileToDelete);
        }
    }
}