    using System.Globalization;
    using System.Reflection;
    using Blog.Entity.DTOs.Categories;
    using Blog.Service.AutoMapper;
    using Blog.Service.FluentValidations;
    using Blog.Service.Helpers.Images;
    using Blog.Service.Services.Concretes;
    using Blog.Service.Services.Contracts;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Http;
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

        public static IServiceCollection ArticleValidatorExtension(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
                options.DisableDataAnnotationsValidation = true;
                options.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
            });
        
            return services;
        }

        public static IServiceCollection LoggedUserExtension(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
        public static IServiceCollection ImageUploadedExtension(this IServiceCollection services)
        {
            services.AddScoped<IImageHelper, ImageHelper>();
            return services;
        }
        public static IServiceCollection UserServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
        
    }