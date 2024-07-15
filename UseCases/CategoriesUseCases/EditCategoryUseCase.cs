using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class EditCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public EditCategoryUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void Execute(int categoryId, Category category)
    {
        _categoryRepository.UpdateCategory(categoryId, category);
    }
}