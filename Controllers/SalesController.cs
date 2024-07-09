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

    public IActionResult SellProductPartial(int productId)
    {
        var product = ProductRepository.GetProductById(productId);
        return PartialView("_SellProduct", product);
    }

    public IActionResult Sell(SalesViewModel salesViewModel)
    {
        if (ModelState.IsValid)
        {
            //Sell the product
        }

        return View("Index", salesViewModel);
    }
}