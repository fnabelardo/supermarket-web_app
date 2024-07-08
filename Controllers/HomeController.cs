using Microsoft.AspNetCore.Mvc;

namespace MVCCourse.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}