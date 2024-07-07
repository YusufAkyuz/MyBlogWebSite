using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapings;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(new Category()
        {
            
            Id = Guid.Parse("4F31A2F0-7CBB-4217-9BB2-E613266D592F"),
            Name = "ASP:NET Core",
            CreatedBy = "Admin Test",
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false   
            
        }, 
            new Category()
            {
                Id = Guid.Parse("E0995B46-20AE-4507-BE7B-E1511FF29A6F"),
                Name = "Spring Boot",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            });
        
        
    }
}