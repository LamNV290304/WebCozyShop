using Microsoft.AspNetCore.Mvc;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services;
using WebCozyShop.Filter;

namespace WebCozyShop.Controllers
{
    [SessionAuthorize]
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
            var viewModel = _variantService.GetProductVariantsPaged(productId, search, page, pageSize);
            if (viewModel == null || viewModel.Product == null)
            {
                TempData["Error"] = "Product not found!";
                return RedirectToAction("ProductList", "Product");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVariant(ProductVariant variant, IFormFile? ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var imageUrl = await _cloudinaryService.UploadImageAsync(ImageFile);
                    if (imageUrl != null)
                        variant.ImageUrl = imageUrl;
                }
                var result = _variantService.AddProductVariant(variant);

                if (!result)
                {
                    TempData["Error"] = "Add product unsuccess";
                    return RedirectToAction("ProductVariantList", new { productId = variant.ProductId });
                }

                TempData["Success"] = "Add product succes";
                return RedirectToAction("ProductVariantList", new { productId = variant.ProductId });
            }
            catch (ArgumentException ae)
            {
                TempData["Error"] = ae.Message;
                return RedirectToAction("ProductVariantList", new { productId = variant.ProductId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while adding the product variant: {ex.Message}";
                return RedirectToAction("ProductVariantList", new { productId = variant.ProductId });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateVariant(ProductVariant variant, IFormFile? ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var imageUrl = await _cloudinaryService.UploadImageAsync(ImageFile);
                    if (imageUrl != null)
                        variant.ImageUrl = imageUrl;
                }

                var result = _variantService.UpdateProductVariant(variant);

                if (!result)
                {
                    TempData["Error"] = "Failed to update product variant.";
                }
                else
                {
                    TempData["Success"] = "Product variant updated successfully!";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error occurred: {ex.Message}";
            }

            return RedirectToAction("ProductVariantList", new { productId = variant.ProductId });
        }


        [HttpPost]
        public IActionResult DeleteVariant(int variantId, int productId)
        {
            var result = _variantService.DeleteProductVariant(variantId, productId);
            if (!result)
            {
                TempData["Error"] = "Product variant deleted unsuccessfully!";
                return RedirectToAction("ProductVariantList", new { productId = productId });
            }

            TempData["Success"] = "Product variant deleted successfully!";
            return RedirectToAction("ProductVariantList", new { productId = productId });
        }

        [HttpGet]
        public IActionResult ImportProducts()
        {
            var model = new ImportProductListViewModel
            {
                Items = new List<ImportItemViewModel> {
                    new ImportItemViewModel()
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ImportProducts(ImportProductListViewModel model)
        {
            var validatedItems = _variantService.ValidateImport(model.Items);

            if (validatedItems.Any(i => !string.IsNullOrEmpty(i.Error)))
            {
                model.Items = validatedItems;
                TempData["Error"] = "❌ Import failed due to invalid SKU(s).";
                return View("ImportView", model);
            }

            _variantService.ImportProducts(validatedItems);

            TempData["Success"] = "📦 Import successful!";
            return RedirectToAction("ImportView");
        }

    }
}