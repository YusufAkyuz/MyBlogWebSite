using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Blog.Service.Services.Concretes;

namespace Blog.Service.Services.Contracts;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<CategoryDto>> GetAllCategoriesNonDeletedAsync()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
        var map = _mapper.Map<List<CategoryDto>>(categories);
        return map;
    }
}