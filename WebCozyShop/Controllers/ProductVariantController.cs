using Microsoft.AspNetCore.Mvc;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;

namespace WebCozyShop.Controllers
{
    public class ProductVariantController : Controller
    {
        private readonly IProductVariantService _variantService;
        private readonly IProductRepository _productRepository;

        private readonly ICloudinaryService _cloudinaryService;

        public ProductVariantController(
            IProductVariantService variantService,
            IProductRepository productRepository,
            ICloudinaryService cloudinaryService)
        {
            _variantService = variantService;
            _productRepository = productRepository;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public IActionResult ProductVariantList(int productId, string search = "", int page = 1, int pageSize = 10)
        {
            try
            {
                var product = _productRepository.GetProductById(productId);
                if (product == null)
                {
                    TempData["Error"] = "Product not found.";
                    return RedirectToAction("ProductList", "Product");
                }

                var allVariants = product.ProductVariants?.ToList() ?? new List<ProductVariant>();

                if (!string.IsNullOrEmpty(search))
                {
                    allVariants = allVariants.Where(v =>
                        (v.Sku != null && v.Sku.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (v.Color != null && v.Color.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (v.Size != null && v.Size.Contains(search, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                var totalVariants = allVariants.Count;
                var totalPages = (int)Math.Ceiling(totalVariants / (double)pageSize);
                var pagedVariants = allVariants
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var viewModel = new ProductVariantManagementViewModel
                {
                    Product = product,
                    PagedVariants = pagedVariants,
                    Search = search,
                    CurrentPage = page,
                    TotalPages = totalPages,
                    TotalVariants = totalVariants,
                    PageSize = pageSize
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error loading product variants: " + ex.Message;
                return RedirectToAction("ProductList", "Product");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVariant(ProductVariant variant, IFormFile? ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(ImageFile);
                if (imageUrl != null)
                    variant.ImageUrl = imageUrl;
            }
            var result = _variantService.CreateVariant(variant);
            TempData[result.Success ? "Success" : "Error"] = result.Success ? "Product variant created successfully!" : result.Error;
            return RedirectToAction("ProductVariantList", new { productId = variant.ProductId });
}

        [HttpPost]
        public async Task<IActionResult> UpdateVariant(ProductVariant variant, IFormFile? ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(ImageFile);
                if (imageUrl != null)
                    variant.ImageUrl = imageUrl;
            }
            var result = _variantService.UpdateVariant(variant);
            TempData[result.Success ? "Success" : "Error"] = result.Success ? "Product variant updated successfully!" : result.Error;
            return RedirectToAction("ProductVariantList", new { productId = variant.ProductId });
        }

        [HttpPost]
        public IActionResult DeleteVariant(int variantId, int productId)
        {
            var result = _variantService.DeleteVariant(variantId, productId);
            if (result.Success)
                TempData["Success"] = "Product variant deleted successfully!";
            else
                TempData["Error"] = result.Error;

            return RedirectToAction("ProductVariantList", new { productId = productId });
        }

        [HttpPost]
        public IActionResult SoftDeleteVariant(int variantId, int productId)
        {
            var result = _variantService.SoftDeleteVariant(variantId, productId);
            if (result.Success)
                TempData["Success"] = "Product variant deactivated successfully!";
            else
                TempData["Error"] = result.Error;

            return RedirectToAction("ProductVariantList", new { productId = productId });
        }

        [HttpPost]
        public IActionResult UpdateStock(int variantId, int productId, int newStock)
        {
            var result = _variantService.UpdateStock(variantId, newStock);
            if (result.Success)
                TempData["Success"] = $"Stock updated to {newStock} successfully!";
            else
                TempData["Error"] = result.Error;

            return RedirectToAction("ProductVariantList", new { productId = productId });
        }

        [HttpPost]
        public IActionResult ToggleStatus(int variantId, int productId, bool isActive)
        {
            var result = _variantService.ToggleStatus(variantId, isActive);
            if (result.Success)
            {
                var status = isActive ? "activated" : "deactivated";
                TempData["Success"] = $"Variant {status} successfully!";
            }
            else
            {
                TempData["Error"] = result.Error;
            }

            return RedirectToAction("ProductVariantList", new { productId = productId });
        }

        [HttpGet]
        public IActionResult GetVariantStock(int variantId)
        {
            try
            {
                // You may want to move this to the service as well
                var variant = _productRepository.GetProductById(variantId)?.ProductVariants?.FirstOrDefault(v => v.VariantId == variantId);
                if (variant != null)
                {
                    return Json(new { success = true, stock = variant.StockQuantity });
                }
                return Json(new { success = false, message = "Variant not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult CheckSkuAvailability(string sku, int? excludeVariantId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(sku))
                {
                    return Json(new { available = false, message = "SKU cannot be empty" });
                }

                var isAvailable = _variantService.IsSkuAvailable(sku, excludeVariantId);
                return Json(new { available = isAvailable, message = isAvailable ? "SKU is available" : "SKU already exists" });
            }
            catch (Exception ex)
            {
                return Json(new { available = false, message = ex.Message });
            }
        }
    }
}