using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapings;

public class ImageMap : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasData(new Image()
        {
            Id = Guid.Parse("F02F448C-6B36-4C2A-ADA7-7218CE5DBDC9"),
            FileName = "images/test_image.jpg",
            FileType = "jpg",
            CreatedBy = "Admin Test",
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        },
            new Image()
            {
                Id = Guid.Parse("CFD94349-46D5-4A14-B6AD-8B1B83A4576E"),
                FileName = "images/test_image.jpg",
                FileType = "jpg",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            });
    }
}