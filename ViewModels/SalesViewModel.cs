using System.ComponentModel.DataAnnotations;
using MVCCourse.Models;

namespace MVCCourse.ViewModels;

public class SalesViewModel
{
    public int SelectedCategoryId { get; set; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    public int SelectedProductId { get; set; }
    [Display(Name = "Quantity")] public int QuantityToSell { get; set; }
}