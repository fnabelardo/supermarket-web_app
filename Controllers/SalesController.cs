using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using MVCCourse.ViewModels;

namespace MVCCourse.Controllers;

public class SalesController : Controller
{
    // GET
    public IActionResult Index()
    {
        var salesViewModel = new SalesViewModel()
        {
            Categories = CategoriesRepository.GetCategories()
        };
        return View(salesViewModel);
    }
}