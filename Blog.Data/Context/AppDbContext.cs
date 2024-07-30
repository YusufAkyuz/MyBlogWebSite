using System.Reflection;
using Blog.Data.Mapings;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
    }   

    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<ArticleVisitors> ArticleVisitors { get; set; } // Düzeltme burada

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Many-to-Many ilişki konfigürasyonu
        modelBuilder.Entity<ArticleVisitors>()
            .HasKey(av => new { av.ArticleId, av.VisitorId });

        modelBuilder.Entity<ArticleVisitors>()
            .HasOne(av => av.Article)
            .WithMany(a => a.ArticleVisitors)
            .HasForeignKey(av => av.ArticleId);

        modelBuilder.Entity<ArticleVisitors>()
            .HasOne(av => av.Visitor)
            .WithMany(v => v.ArticleVisitors)
            .HasForeignKey(av => av.VisitorId);
    }
}