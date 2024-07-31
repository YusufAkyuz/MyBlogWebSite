using AutoMapper;
using Blog.Entity.DTOs.Comments;
using Blog.Entity.Entities;

namespace Blog.Service.AutoMapper;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CommentAddDto>().ReverseMap();
    }
}