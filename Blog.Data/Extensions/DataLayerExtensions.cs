using Blog.Data.Context;
using Blog.Data.Repositories.Concretes;
using Blog.Data.Repositories.Contracts;
using Blog.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Data.Extensions;

public static class DataLayerExtensions
{
    //DI yapıyoruz IUnitOfWork diyince sen UnitOfWork anla ve ordan obje oluştur diyerek bağımlılığı azaltmaya çalışrız  soliD

    public static IServiceCollection RepositoryExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }

    public static IServiceCollection DbContextExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
        return services;
    }

    //DI yapıyoruz IUnitOfWork diyince sen UnitOfWork anla ve ordan obje oluştur diyerek bağımlılığı azaltmaya çalışrız  soliD
    public static IServiceCollection UnitOfWorkExtension(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        return services;
    }
    
}