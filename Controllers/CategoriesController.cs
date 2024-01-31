using Microsoft.AspNetCore.Mvc;

namespace MVCCourse.Controllers;

public class CategoriesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}