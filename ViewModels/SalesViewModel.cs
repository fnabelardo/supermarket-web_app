using System.ComponentModel.DataAnnotations;
using MVCCourse.Models;
using MVCCourse.ViewModels.Validations;

namespace MVCCourse.ViewModels;

public class SalesViewModel
{
    public int SelectedCategoryId { get; set; }
    public IEnumerable<CoreBusiness.Category> Categories { get; set; } = new List<CoreBusiness.Category>();
    public int SelectedProductId { get; set; }

    [Range(1, int.MaxValue)]
    [Display(Name = "Quantity")]
    [SalesViewModel_EnsureProperQuantity]
    public int QuantityToSell { get; set; }
}