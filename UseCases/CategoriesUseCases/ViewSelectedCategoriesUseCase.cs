using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class ViewSelectedCategoriesUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public ViewSelectedCategoriesUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Category Execute(int categoryId)
    {
        return _categoryRepository.GetCategoryById(categoryId);
    }
}