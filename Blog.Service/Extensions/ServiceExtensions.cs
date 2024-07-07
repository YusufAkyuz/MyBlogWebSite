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
}