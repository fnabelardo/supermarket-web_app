using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using MVCCourse.ViewModels;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;

namespace MVCCourse.Controllers;

public class SalesController : Controller
{
    private readonly IViewSelectedProductsUseCase _viewSelectedProductsUseCase;
    private readonly IViewCategoriesUseCase _viewCategoriesUseCase;
    private readonly IEditProductUseCase _editProductUseCase;
    private readonly IRecordTransactionUseCase _recordTransactionUseCase;
    private readonly ISellProductUseCase _sellProductUseCase;

    public SalesController(IViewSelectedProductsUseCase viewSelectedProductsUseCase,
        IViewCategoriesUseCase viewCategoriesUseCase, IEditProductUseCase editProductUseCase,
        IRecordTransactionUseCase recordTransactionUseCase, ISellProductUseCase sellProductUseCase)
    {
        _viewSelectedProductsUseCase = viewSelectedProductsUseCase;
        _viewCategoriesUseCase = viewCategoriesUseCase;
        _editProductUseCase = editProductUseCase;
        _recordTransactionUseCase = recordTransactionUseCase;
        _sellProductUseCase = sellProductUseCase;
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
        if (ModelState.IsValid)
        {
            //Sell the product
            _sellProductUseCase.Execute("Cashier1", salesViewModel.SelectedProductId,
                salesViewModel.QuantityToSell);
        }

        var product = _viewSelectedProductsUseCase.Execute(salesViewModel.SelectedProductId);
        salesViewModel.SelectedCategoryId = product?.CategoryId == null ? 0 : product.CategoryId.Value;
        salesViewModel.Categories = _viewCategoriesUseCase.Execute();

        return View("Index", salesViewModel);
    }
}