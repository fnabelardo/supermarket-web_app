using Microsoft.AspNetCore.Mvc;

namespace MVCCourse.Controllers;

public class SalesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}