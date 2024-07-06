using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using MVCCourse.ViewModels;

namespace MVCCourse.Controllers;

public class ProductsController : Controller
{
    // GET
    public IActionResult Index()
    {
        var products = ProductRepository.GetProducts(loadCategory: true);
        return View(products);
    }

    // Model Binding - https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-8.0
    // [DataComeFrom]
    // Example: [FromQuery], [FromRoute], [FromForm], [FromBody], [FromHeader]
    // GET
    public IActionResult Edit(int? id)
    {
        ViewBag.Action = "Edit";
        var product = ProductRepository.GetProductById(id.HasValue ? id.Value : 0); //## id.HasValue ? id.Value : 0 => id ?? 0 (Null-coalescing expression) 
        return View(product);
    }
    
    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            ProductRepository.UpdateProduct(product.ProductId, product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
    
    public IActionResult Add()
    {
        ViewBag.Action = "Add";
        var productViewModel = new ProductViewModel()
        {
            Categories = CategoriesRepository.GetCategories()
        };
        
        return View(productViewModel);
    }
    
    [HttpPost]
    public IActionResult Add(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            ProductRepository.AddProduct(productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Action = "Add";
        productViewModel.Categories = CategoriesRepository.GetCategories();
        return View(productViewModel);
    }
    
    public IActionResult Delete(int productId)
    {
        ProductRepository.DeleteProduct(productId);
        return RedirectToAction(nameof(Index));
    }
    
}