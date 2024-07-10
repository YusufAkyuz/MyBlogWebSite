    using System.Reflection;
    using Blog.Entity.DTOs.Categories;
    using Blog.Service.AutoMapper;
    using Blog.Service.Services.Concretes;
    using Blog.Service.Services.Contracts;
    using Microsoft.Extensions.DependencyInjection;

    namespace Blog.Service.Extensions;

    public static class ServiceExtensions
    {
        public static IServiceCollection ArticleServiceExtension(this IServiceCollection services)
        {
            services.AddScoped(typeof(IArticleService), typeof(ArticleService));

            return services;
        }

        public static IServiceCollection ArticleDtoExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            return services;
        }

        public static IServiceCollection CategoryDtoExtension(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }

        
    }