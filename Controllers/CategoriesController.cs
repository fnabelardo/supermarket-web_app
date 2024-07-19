using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using UseCases.CategoriesUseCases;

namespace MVCCourse.Controllers;

public class CategoriesController : Controller
{
    private readonly IViewCategoriesUseCase _viewCategoriesUseCase;

    public CategoriesController(IViewCategoriesUseCase viewCategoriesUseCase)
    {
        _viewCategoriesUseCase = viewCategoriesUseCase;
    }
    
    // GET
    public IActionResult Index()
    {
        var categories = _viewCategoriesUseCase.Execute();
        return View(categories);
    }

    // Model Binding - https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-8.0
    // [DataComeFrom]
    // Example: [FromQuery], [FromRoute], [FromForm], [FromBody], [FromHeader]
    // GET
    public IActionResult Edit(int? id)
    {
        ViewBag.Action = "Edit";
        var category =
            CategoriesRepository.GetCategoryById(id.HasValue
                ? id.Value
                : 0); //## id.HasValue ? id.Value : 0 => id ?? 0 (Null-coalescing expression) 
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            CategoriesRepository.UpdateCategory(category.CategoryId, category);
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public IActionResult Add()
    {
        ViewBag.Action = "Add";
        return View();
    }

    [HttpPost]
    public IActionResult Add(Category category)
    {
        if (ModelState.IsValid)
        {
            CategoriesRepository.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public IActionResult Delete(int categoryId)
    {
        CategoriesRepository.DeleteCategory(categoryId);
        return RedirectToAction(nameof(Index));
    }
}