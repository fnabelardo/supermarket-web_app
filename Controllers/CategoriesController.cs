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

    // GET
    public IActionResult Edit(int? id)
    {
        var category = new Category { CategoryId = id.HasValue ? id.Value : 0 }; //## id.HasValue ? id.Value : 0 => id ?? 0 (Null-coalescing expression)
        return View(category);
    }
}