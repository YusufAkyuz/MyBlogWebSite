using Blog.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents;

public class HomeCategoriesViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public HomeCategoriesViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _categoryService.Get24CategoriesDeletedAsync();
        return View(categories);
    }
}