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
        var product = ProductRepository.GetProductById(salesViewModel.SelectedProductId);

        if (ModelState.IsValid)
        {
            //Sell the product
            if (product != null)
            {
                TransactionsRepository.Add(
                    "Cashier1",
                    salesViewModel.SelectedProductId,
                    product.Name,
                    product.Price.HasValue ? product.Price.Value : 0,
                    product.Quantity.HasValue ? product.Quantity.Value : 0,
                    salesViewModel.QuantityToSell);

                product.Quantity -= salesViewModel.QuantityToSell;
                ProductRepository.UpdateProduct(salesViewModel.SelectedProductId, product);
            }
        }

        salesViewModel.SelectedCategoryId = product?.CategoryId == null ? 0 : product.CategoryId.Value;
        salesViewModel.Categories = CategoriesRepository.GetCategories();
        return View("Index", salesViewModel);
    }
}