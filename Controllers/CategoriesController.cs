using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;

namespace MVCCourse.Controllers;

public class CategoriesController : Controller
{
    // GET
    public IActionResult Index()
    {
        var categories = CategoriesRepository.GetCategories();
        return View(categories);
    }

    // Model Binding - https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-8.0
    // [DataComeFrom]
    // Example: [FromQuery], [FromRoute], [FromForm], [FromBody], [FromHeader]
    // GET
    public IActionResult Edit(int? id)
    {
        var category = CategoriesRepository.GetCategoryById(id.HasValue ? id.Value : 0); //## id.HasValue ? id.Value : 0 => id ?? 0 (Null-coalescing expression) 
        // var category = new Category { CategoryId = id.HasValue ? id.Value : 0 }; //## id.HasValue ? id.Value : 0 => id ?? 0 (Null-coalescing expression)
        return View(category);
    }
}