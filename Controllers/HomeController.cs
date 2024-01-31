using Microsoft.AspNetCore.Mvc;

namespace MVCCourse.Properties.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

}