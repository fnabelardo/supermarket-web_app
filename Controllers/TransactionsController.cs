using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using MVCCourse.ViewModels;
using UseCases.TransactionsUseCases;
using Transaction = System.Transactions.Transaction;

namespace MVCCourse.Controllers;

public class TransactionsController : Controller
{
    private readonly ISearchTransactionsUseCase _searchTransactionsUseCase;

    public TransactionsController(ISearchTransactionsUseCase searchTransactionsUseCase)
    {
        _searchTransactionsUseCase = searchTransactionsUseCase;
    }
    
    // GET
    public IActionResult Index()
    {
        var transactionsViewModel = new TransactionsViewModel();
        var transactions = _searchTransactionsUseCase.Execute("", DateTime.Now.AddDays(-365), DateTime.Now);
        transactionsViewModel.Transactions = transactions;
        return View(transactionsViewModel);
    }

    public IActionResult Search(TransactionsViewModel transactionsViewModel)
    {
        var transactions = _searchTransactionsUseCase.Execute(
            transactionsViewModel.CashierName ?? string.Empty,
            transactionsViewModel.StartDate,
            transactionsViewModel.EndDate
        );
        transactionsViewModel.Transactions = transactions;
        return View("Index", transactionsViewModel);
    }
}