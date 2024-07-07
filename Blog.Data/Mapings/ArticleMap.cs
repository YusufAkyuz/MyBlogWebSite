using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapings;

public class ArticleMap : IEntityTypeConfiguration<Article>
{
    
    //Oluşturulan tablo içeriğindeki propertylerin değerlerini güncelliyor olucağız
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        //Aşağıdaki verilerin alabileceği değerler konusunda sınırlamalar ek bilgiler sağlayabiliriz 
        builder.Property(x => x.Title).HasMaxLength(150);

        builder.HasData(new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Asp net core deneme makalesi",
            Content = "Merhaba bu makale asp.net core üzerine yazılmış bir makaledir.",
            ViewCount = 15,
            CategoryId = Guid.Parse("4F31A2F0-7CBB-4217-9BB2-E613266D592F"),
            ImageId = Guid.Parse("F02F448C-6B36-4C2A-ADA7-7218CE5DBDC9"),
            CreatedBy = "Admin Test",
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        },
            
        new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Spring Boot deneme makalesi",
            Content = "Merhaba bu makale Java Spring üzerine yazılmış bir makaledir.",
            ViewCount = 15,
            CategoryId = Guid.Parse("E0995B46-20AE-4507-BE7B-E1511FF29A6F"),
            ImageId = Guid.Parse("CFD94349-46D5-4A14-B6AD-8B1B83A4576E"),
            CreatedBy = "Admin Test",
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        }
        );
        
    }
}