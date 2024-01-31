using Microsoft.AspNetCore.Mvc;

namespace MVCCourse.Controllers;

public class CategoriesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    // GET
    public IActionResult Edit(int? id)
    {
        if (id != null)
        {
            return new ContentResult { Content = id.ToString() };
        }
        else
        {
            return new ContentResult { Content = "null content" };
        }



    }
}