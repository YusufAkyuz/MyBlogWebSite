using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Service.Services.Contracts;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _accessor;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _accessor = accessor;
    }
    public async Task<List<CategoryDto>> GetAllCategoriesNonDeletedAsync()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
        var map = _mapper.Map<List<CategoryDto>>(categories);
        return map;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesDeletedAsync()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
        var map = _mapper.Map<List<CategoryDto>>(categories);
        return map;
    }

    public async Task<List<CategoryDto>> Get24CategoriesDeletedAsync()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
        var map = _mapper.Map<List<CategoryDto>>(categories);
        return map.Take(24).ToList();
    }

    public async Task Add(CategoryAddDto categoryAddDto)
    {
        var userName = _accessor.HttpContext.User.GetLoggedInUserEmail();
        var category = new Category(categoryAddDto.Name, userName);
        await _unitOfWork.GetRepository<Category>().AddAsync(category);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Category> GetCatgoryById(Guid categoryId)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(categoryId);
        return category;
    }

    public async Task Update(CategoryUpdateDto categoryUpdateDto)
    {
        var userEmail = _accessor.HttpContext.User.GetLoggedInUserEmail();
        var category = await _unitOfWork.GetRepository<Category>().GetAsync(x=>!x.IsDeleted && x.Id == categoryUpdateDto.Id);

        category.Name = categoryUpdateDto.Name;
        category.ModifiedBy = userEmail;
        category.ModifiedDate = DateTime.UtcNow;
        
        await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
        await _unitOfWork.SaveAsync();
    }

    public async Task<string> SafeDeleteCategoryAsync(Guid id)
    {
        var userEmail = _accessor.HttpContext.User.GetLoggedInUserEmail();
        var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
        
        category.ModifiedDate = DateTime.UtcNow;
        category.DeletedBy = userEmail;
        category.IsDeleted = true;

        await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
        await _unitOfWork.SaveAsync();
        
        return category.Name;
    }

    public async Task<string> UndoDeleteCategoryAsync(Guid id)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
        
        category.ModifiedDate = DateTime.UtcNow;
        category.DeletedBy = null;
        category.IsDeleted = false;

        await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
        await _unitOfWork.SaveAsync();
        
        return category.Name;
    }
}