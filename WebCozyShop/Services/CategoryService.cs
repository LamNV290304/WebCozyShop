using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services.Interface;

namespace WebCozyShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
        public Category? GetCategoryById(int id)
        {
            return _categoryRepository.GetCategoryById(id);
        }
        public bool CreateCategory(string category)
        {
            return _categoryRepository.CreateCategory(category);
        }
        public bool UpdateCategory(string category, int id)
        {
            var cate = new Category
            {
                CategoryId = id,
                Name = category
            };
            return _categoryRepository.UpdateCategory(cate);
        }
        public bool DeleteCategory(int id)
        {
            return _categoryRepository.DeleteCategory(id);
        }

    }
}
