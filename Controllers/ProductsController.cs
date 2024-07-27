using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using MVCCourse.ViewModels;
using UseCases.ProductsUseCases;

namespace MVCCourse.Controllers;

public class ProductsController : Controller
{
    private readonly IViewProductsUseCase _viewProductsUseCase;

    public ProductsController(IViewProductsUseCase viewProductsUseCase)
    {
        _viewProductsUseCase = viewProductsUseCase;
    }
    
    // GET
    public IActionResult Index()
    {
        var products = _viewProductsUseCase.Execute(loadCategory: true);
        return View(products as List<CoreBusiness.Product>);
    }

    // Model Binding - https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-8.0
    // [DataComeFrom]
    // Example: [FromQuery], [FromRoute], [FromForm], [FromBody], [FromHeader]
    // GET
    public IActionResult Edit(int? id)
    {
        ViewBag.Action = "Edit";
        var productViewModel = new ProductViewModel
        {
            Categories = CategoriesRepository.GetCategories(),
            Product = ProductRepository.GetProductById(id ?? 0) ?? new Product()
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            ProductRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "Edit";
        productViewModel.Categories = CategoriesRepository.GetCategories();
        return View(productViewModel);
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

    public IActionResult ProductsByCategoryPartial(int categoryId)
    {
        var products = ProductRepository.GetProductsByCategoryId(categoryId);
        return PartialView("_Products", products);
    }
}