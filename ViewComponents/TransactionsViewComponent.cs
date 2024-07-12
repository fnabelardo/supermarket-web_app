using Microsoft.AspNetCore.Mvc;

namespace MVCCourse.ViewComponents;

[ViewComponent]
public class TransactionsViewComponent : ViewComponent
{
    public string Invoke()
    {
        return "List of Transactions";
    }
}