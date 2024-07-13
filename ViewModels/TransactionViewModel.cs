using System.ComponentModel.DataAnnotations;
using MVCCourse.Models;

namespace MVCCourse.ViewModels;

public class TransactionsViewModel
{
    [Display(Name = "Cashier Name")] public string? CashierName { get; set; }

    [Display(Name = "Start Date")] public DateTime StartDate { get; set; } = DateTime.Now;

    [Display(Name = "End Date")] public DateTime EndDate { get; set; } = DateTime.Now;

    public IEnumerable<Transaction> Transactions { get; set; }
}