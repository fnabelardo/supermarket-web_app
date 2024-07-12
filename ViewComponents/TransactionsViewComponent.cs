using Microsoft.AspNetCore.Mvc;
using MVCCourse.Models;

namespace MVCCourse.ViewComponents;

[ViewComponent]
public class TransactionsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string userName)
    {
        var transactions = TransactionsRepository.GetByDayAndCashier(DateTime.Now, userName);
        return View(transactions);
    }
}