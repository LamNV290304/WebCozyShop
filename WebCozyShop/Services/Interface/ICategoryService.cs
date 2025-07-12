using WebCozyShop.Models;

namespace WebCozyShop.Services.Interface
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category? GetCategoryById(int id);
        bool CreateCategory(string category);
        bool UpdateCategory(string category, int id);
        bool DeleteCategory(int id);
    }
}
