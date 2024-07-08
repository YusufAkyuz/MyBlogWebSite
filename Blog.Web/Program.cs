    using Blog.Data.Extensions;
    using Blog.Service.Extensions;


    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.DbContextExtension(builder.Configuration);
    builder.Services.RepositoryExtension(builder.Configuration);
    builder.Services.UnitOfWorkExtension();
    builder.Services.ArticleServiceExtension();
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.ArticleDtoExtensions();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
        defaults: new { area = "Admin" }
    );

    app.MapDefaultControllerRoute();

    app.Run();