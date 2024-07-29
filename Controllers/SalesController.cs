using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using MVCCourse.ViewModels;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;

namespace MVCCourse.Controllers;

public class SalesController : Controller
{
    private readonly IViewSelectedProductsUseCase _viewSelectedProductsUseCase;
    private readonly IViewCategoriesUseCase _viewCategoriesUseCase;
    private readonly IEditProductUseCase _editProductUseCase;

    public SalesController(IViewSelectedProductsUseCase viewSelectedProductsUseCase,
        IViewCategoriesUseCase viewCategoriesUseCase, IEditProductUseCase editProductUseCase)
    {
        _viewSelectedProductsUseCase = viewSelectedProductsUseCase;
        _viewCategoriesUseCase = viewCategoriesUseCase;
        _editProductUseCase = editProductUseCase;
    }

    // GET
    public IActionResult Index()
    {
        var salesViewModel = new SalesViewModel()
        {
            Categories = _viewCategoriesUseCase.Execute()
        };
        return View(salesViewModel);
    }

    public IActionResult SellProductPartial(int productId)
    {
        var product = _viewSelectedProductsUseCase.Execute(productId);
        return PartialView("_SellProduct", product);
    }

    public IActionResult Sell(SalesViewModel salesViewModel)
    {
        var product = _viewSelectedProductsUseCase.Execute(salesViewModel.SelectedProductId);

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
                _editProductUseCase.Execute(salesViewModel.SelectedProductId, product);
            }
        }

        salesViewModel.SelectedCategoryId = product?.CategoryId == null ? 0 : product.CategoryId.Value;
        salesViewModel.Categories = _viewCategoriesUseCase.Execute();
        return View("Index", salesViewModel);
    }
}