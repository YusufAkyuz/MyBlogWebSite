using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapings;

public class ArticleVisitorMap : IEntityTypeConfiguration<ArticleVisitors>
{
    public void Configure(EntityTypeBuilder<ArticleVisitors> builder)
    {
        builder.HasKey(x => new { x.ArticleId, x.VisitorId });
    }
}