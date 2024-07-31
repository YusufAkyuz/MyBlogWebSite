using AutoMapper;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Service.AutoMapper;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<ArticleDto, Article>().ReverseMap();  //Burdaki reverse metodu sayesinde mapleme işleminin tam terside geçerli
        CreateMap<ArticleAddDto, Article>().ReverseMap();
        CreateMap<ArticleUpdateDto, ArticleDto>().ReverseMap();
        CreateMap<ArticleUpdateDto, Article>().ReverseMap();
        CreateMap<ArticleUpdateDto, Article>().ReverseMap();
    }
}