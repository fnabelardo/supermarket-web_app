using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces;

public interface ICategoryRepository
{
    void AddCategory(int categoryId);
    IEnumerable<Category> GetCategories();
    Category GetCategoryById(int categoryId);
    void UpdateCategory(int categoryId, Category category);
    void DeleteCategory(int categoryId);
}