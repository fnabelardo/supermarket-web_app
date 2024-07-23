using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using UseCases.CategoriesUseCases;

namespace MVCCourse.Controllers;

public class CategoriesController : Controller
{
    private readonly IViewCategoriesUseCase _viewCategoriesUseCase;
    private readonly IViewSelectedCategoriesUseCase _viewSelectedCategoriesUseCase;
    private readonly IEditCategoryUseCase _editCategoryUseCase;
    private readonly IAddCategoryUseCase _addCategoryUseCase;

    public CategoriesController(IViewCategoriesUseCase viewCategoriesUseCase,
        IViewSelectedCategoriesUseCase viewSelectedCategoriesUseCase,
        IEditCategoryUseCase editCategoryUseCase, IAddCategoryUseCase addCategoryUseCase)
    {
        _viewCategoriesUseCase = viewCategoriesUseCase;
        _viewSelectedCategoriesUseCase = viewSelectedCategoriesUseCase;
        _editCategoryUseCase = editCategoryUseCase;
        _addCategoryUseCase = addCategoryUseCase;
    }

    // GET
    public IActionResult Index()
    {
        var categories = _viewCategoriesUseCase.Execute();
        return View(categories as List<CoreBusiness.Category>);
    }

    // Model Binding - https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-8.0
    // [DataComeFrom]
    // Example: [FromQuery], [FromRoute], [FromForm], [FromBody], [FromHeader]
    // GET
    public IActionResult Edit(int? id)
    {
        ViewBag.Action = "Edit";
        var category = _viewSelectedCategoriesUseCase.Execute(id ?? 0);
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(CoreBusiness.Category category)
    {
        if (ModelState.IsValid)
        {
            _editCategoryUseCase.Execute(category.CategoryId, category);
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public IActionResult Add()
    {
        ViewBag.Action = "Add";
        return View();
    }

    [HttpPost]
    public IActionResult Add(CoreBusiness.Category category)
    {
        if (ModelState.IsValid)
        {
            _addCategoryUseCase.Execute(category);
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public IActionResult Delete(int categoryId)
    {
        CategoriesRepository.DeleteCategory(categoryId);
        return RedirectToAction(nameof(Index));
    }
}