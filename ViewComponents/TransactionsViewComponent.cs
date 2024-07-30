using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;
using UseCases.TransactionsUseCases;

namespace MVCCourse.ViewComponents;

[ViewComponent]
public class TransactionsViewComponent : ViewComponent
{
    private readonly IGetByDayAndCashierTransactionsUseCase _getByDayAndCashierTransactionsUseCase;

    public TransactionsViewComponent(IGetByDayAndCashierTransactionsUseCase getByDayAndCashierTransactionsUseCase)
    {
        _getByDayAndCashierTransactionsUseCase = getByDayAndCashierTransactionsUseCase;
    }
    
    public IViewComponentResult Invoke(string userName)
    {
        var transactions = _getByDayAndCashierTransactionsUseCase.Execute(DateTime.Now, userName);
        return View(transactions);
    }
}