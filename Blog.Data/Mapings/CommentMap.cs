using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Comment = Blog.Entity.Entities.Comment;

namespace Blog.Data.Mapings;

public class CommentMap : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasData(new Comment()
        {
            Id = 1,
            Name = "Yusuf",
            Email = "yusufakyus@gmail.com",
            Content = "Merhaba Bu Bir Test Yorumudur"
        });
    }
}