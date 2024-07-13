using Microsoft.AspNetCore.Mvc;
using MVCCourse.ViewModels;

namespace MVCCourse.Controllers;

public class TransactionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        var transactionsViewModel = new TransactionsViewModel();
        return View(transactionsViewModel);
    }
    
    public IActionResult Search()
    {
        return View("Index");
    }
}