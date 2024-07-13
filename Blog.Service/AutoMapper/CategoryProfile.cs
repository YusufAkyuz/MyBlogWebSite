using AutoMapper;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Service.AutoMapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();  //Burdaki reverse metodu sayesinde mapleme işleminin tam terside geçerli
        CreateMap<CategoryAddDto, Category>().ReverseMap();
        CreateMap<CategoryUpdateDto, Category>().ReverseMap();

    }   
}