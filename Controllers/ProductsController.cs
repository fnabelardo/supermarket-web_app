using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using MVCCourse.ViewModels;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;

namespace MVCCourse.Controllers;

public class ProductsController : Controller
{
    private readonly IViewProductsUseCase _viewProductsUseCase;
    private readonly IViewCategoriesUseCase _viewCategoriesUseCase;
    private readonly IViewSelectedProductsUseCase _viewSelectedProductsUseCase;
    private readonly IAddProductUseCase _addProductUseCase;
    private readonly IEditProductUseCase _editProductUseCase;
    private readonly IDeleteProductUseCase _deleteProductUseCase;

    public ProductsController(IViewProductsUseCase viewProductsUseCase, IViewCategoriesUseCase viewCategoriesUseCase,
        IViewSelectedProductsUseCase viewSelectedProductsUseCase, IAddProductUseCase addProductUseCase,
        IEditProductUseCase editProductUseCase, IDeleteProductUseCase deleteProductUseCase)
    {
        _viewProductsUseCase = viewProductsUseCase;
        _viewCategoriesUseCase = viewCategoriesUseCase;
        _viewSelectedProductsUseCase = viewSelectedProductsUseCase;
        _addProductUseCase = addProductUseCase;
        _editProductUseCase = editProductUseCase;
        _deleteProductUseCase = deleteProductUseCase;
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
            Categories = _viewCategoriesUseCase.Execute() as IEnumerable<CoreBusiness.Category>,
            Product = _viewSelectedProductsUseCase.Execute(id.Value, false)
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            _editProductUseCase.Execute(productViewModel.Product.ProductId, productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "Edit";
        productViewModel.Categories = _viewCategoriesUseCase.Execute();
        return View(productViewModel);
    }

    public IActionResult Add()
    {
        ViewBag.Action = "Add";
        var productViewModel = new ProductViewModel()
        {
            Categories = _viewCategoriesUseCase.Execute()
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Add(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            _addProductUseCase.Execute(productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "Add";
        productViewModel.Categories = _viewCategoriesUseCase.Execute();
        return View(productViewModel);
    }

    public IActionResult Delete(int productId)
    {
        _deleteProductUseCase.Execute(productId);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ProductsByCategoryPartial(int categoryId)
    {
        var products = ProductRepository.GetProductsByCategoryId(categoryId);
        return PartialView("_Products", products);
    }
}