using Microsoft.AspNetCore.Mvc;
using WebCozyShop.Models;
using WebCozyShop.Services.Interface;

namespace WebCozyShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult ProductList(string search = "", string selectedCategoryId = "0", int page = 1)
        {
            int categoryId;
            int.TryParse(selectedCategoryId, out categoryId);

            var products = _productService.GetProductsPaged(search, categoryId, page, 10);
            return View(products);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productService.AddProduct(product);
                    TempData["Success"] = "Product created successfully!";
                }
                else
                {
                    TempData["Error"] = "Validation failed.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error creating product: " + ex.Message;
            }

            return RedirectToAction("ProductList");
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productService.UpdateProduct(product);
                    TempData["Success"] = "Product updated successfully!";
                }
                else
                {
                    TempData["Error"] = "Validation failed.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error updating product: " + ex.Message;
            }

            return RedirectToAction("ProductList");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                _productService.DeleteProduct(productId);
                TempData["Success"] = "Product deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error deleting product: " + ex.Message;
            }

            return RedirectToAction("ProductList");
        }

    }
}
