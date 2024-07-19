using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class ViewCategoriesUseCase : IViewCategoriesUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public ViewCategoriesUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> Execute()
    {
        return _categoryRepository.GetCategories();
    }
}