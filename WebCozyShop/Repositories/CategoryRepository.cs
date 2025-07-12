using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;

namespace WebCozyShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CozyShopDbContext _context;
        public CategoryRepository(CozyShopDbContext context)
        {
            _context = context;
        }
        public bool CreateCategory(string category)
        {
            if (category == null) return false;
            var cate = new Category
            {
                Name = category
            };
            _context.Categories.Add(cate);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (category == null) return false;
            _context.Categories.Remove(category);
            return _context.SaveChanges() > 0;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryID == id);
        }

        public bool UpdateCategory(Category category)
        {
            if (category == null) return false;
            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryID == category.CategoryID);
            if (existingCategory == null) return false;
            existingCategory.Name = category.Name;
            _context.Categories.Update(existingCategory);
            return _context.SaveChanges() > 0;
        }
    }
}
