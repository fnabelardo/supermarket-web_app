using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using MVCCourse.ViewModels;

namespace MVCCourse.Controllers;

public class TransactionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        var transactionsViewModel = new TransactionsViewModel();
        var transactions = TransactionsRepository.Search("", DateTime.Now.AddDays(-365), DateTime.Now);
        transactionsViewModel.Transactions = transactions;
        return View(transactionsViewModel);
    }

    public IActionResult Search(TransactionsViewModel transactionsViewModel)
    {
        var transactions = TransactionsRepository.Search(
            transactionsViewModel.CashierName ?? string.Empty,
            transactionsViewModel.StartDate,
            transactionsViewModel.EndDate
        );
        transactionsViewModel.Transactions = transactions;
        return View("Index", transactionsViewModel);
    }
}