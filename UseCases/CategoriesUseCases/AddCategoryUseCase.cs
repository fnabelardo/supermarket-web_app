using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class AddCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public AddCategoryUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public void Execute(int categoryId)
    {
        _categoryRepository.AddCategory(categoryId);
    }
}