using System.ComponentModel.DataAnnotations;
using MVCCourse.Models;
using UseCases.ProductsUseCases;

namespace MVCCourse.ViewModels.Validations;

public class SalesViewModel_EnsureProperQuantity : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var salesViewModel = validationContext.ObjectInstance as SalesViewModel;

        if (salesViewModel != null)
        {
            if (salesViewModel.QuantityToSell <= 0)
            {
                return new ValidationResult("The quantity to sell has to be greater than zero");
            }

            var getProductByIdUseCase =
                validationContext.GetService(typeof(IViewSelectedProductsUseCase)) as IViewSelectedProductsUseCase;

            if (getProductByIdUseCase != null)
            {
                var productToSell = getProductByIdUseCase.Execute(salesViewModel.SelectedProductId);

                if (productToSell != null)
                {
                    if (salesViewModel.QuantityToSell > productToSell.Quantity)
                    {
                        return new ValidationResult(
                            $"{productToSell.Name} only has {productToSell.Quantity} left. It is not enough");
                    }
                }
                else
                {
                    return new ValidationResult("The selected product doesn't exist.");
                }
            }
        }

        return ValidationResult.Success;
    }
}