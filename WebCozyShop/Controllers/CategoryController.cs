using Microsoft.AspNetCore.Mvc;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult CategoriesList()
        {
            var categories = _categoryService.GetAllCategories();
            var vm = new CategoryManagementViewModel
            {
                Categories = categories
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryManagementViewModel model)
        {
            bool isCreated = _categoryService.CreateCategory(model.Name);

            if (isCreated)
            {
                TempData["Success"] = "Category created successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to create category.";
            }

            return RedirectToAction("CategoriesList");
        }

        [HttpPost]
        public IActionResult UpdateCategory(CategoryManagementViewModel model, IFormCollection form)
        {
            int categoryId = int.Parse(form["CategoryID"]);
            bool isUpdated = _categoryService.UpdateCategory(model.Name, categoryId);
            if (isUpdated)
            {
                TempData["Success"] = "Category updated successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to update category.";
            }
            return RedirectToAction("CategoriesList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(IFormCollection form)
        {
            int categoryId = int.Parse(form["CategoryID"]);

            bool isDeleted = _categoryService.DeleteCategory(categoryId);
            if (isDeleted)
            {
                TempData["Success"] = "Category deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete category.";
            }
            return RedirectToAction("CategoriesList");
        }
    }
}
