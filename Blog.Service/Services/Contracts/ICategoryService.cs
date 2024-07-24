using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Concretes;

public interface ICategoryService
{
    public Task<List<CategoryDto>> GetAllCategoriesNonDeletedAsync();
    public Task<List<CategoryDto>> GetAllCategoriesDeletedAsync();
    Task Add(CategoryAddDto categoryAddDto);
    Task<Category> GetCatgoryById(Guid categoryId);
    Task Update(CategoryUpdateDto categoryUpdateDto);
    Task<string>SafeDeleteCategoryAsync(Guid id);
    Task<string>UndoDeleteCategoryAsync(Guid id);
}