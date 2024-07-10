    using Blog.Data.Context;
    using Blog.Data.Extensions;
    using Blog.Entity.Entities;
    using Blog.Service.Extensions;
    using Microsoft.AspNetCore.Identity;


    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.DbContextExtension(builder.Configuration);
    builder.Services.RepositoryExtension(builder.Configuration);
    builder.Services.UnitOfWorkExtension();
    builder.Services.ArticleServiceExtension();
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.ArticleDtoExtension();
    builder.Services.CategoryDtoExtension();

    builder.Services.AddSession();
    builder.Services.AddIdentity<AppUser, AppRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    }).AddRoleManager<RoleManager<AppRole>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();


    builder.Services.ConfigureApplicationCookie(config =>
    {
        config.LoginPath = new PathString("/Admin/Auth/Login");
        config.LogoutPath = new PathString("/Admin/Auth/Logout");
        config.Cookie = new CookieBuilder()
        {
            Name = "Yusuf_Akyuz_Blog_Site",
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            SecurePolicy = CookieSecurePolicy.SameAsRequest
        };
        config.SlidingExpiration = true;
        config.ExpireTimeSpan =
            TimeSpan.FromDays(7); //Giriş yaptı   ktan 7 gün sonrasına kadar oturumun açık kalması sağlanır
        config.AccessDeniedPath =
            new PathString("/Admin/Auth/AccessDenied"); //yetkisiz işlem olunca bu sayfaya atıcak bizi
    });

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseSession();
    app.UseAuthentication();    //--> Kimlik doğruşama işlemlerini kullan dedik artık
    app.UseAuthorization();     //yetkilendirme işlemi her zaman kimlik doğrulama işleminden sonra gelmelidir

    app.MapControllerRoute(
        name: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
        defaults: new { area = "Admin" }
    );

    app.MapDefaultControllerRoute();

    app.Run();