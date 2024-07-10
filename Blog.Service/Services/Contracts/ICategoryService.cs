using Blog.Entity.DTOs.Categories;

namespace Blog.Service.Services.Concretes;

public interface ICategoryService
{
    public Task<List<CategoryDto>> GetAllCategoriesNonDeletedAsync();
}