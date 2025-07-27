using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services.Interface;
using WebCozyShop.Helper;

namespace WebCozyShop.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _variantRepo;
        private readonly IProductRepository _productRepo;

        public ProductVariantService(IProductVariantRepository variantRepo, IProductRepository productRepo)
        {
            _variantRepo = variantRepo;
            _productRepo = productRepo;
        }

        public (bool Success, string Error, ProductVariant? Variant) CreateVariant(ProductVariant variant)
        {
            if (variant.ProductId <= 0)
                return (false, "Invalid product ID.", null);

            if (variant.Price <= 0)
                return (false, "Price must be greater than 0.", null);

            // Auto-generate SKU if empty
            if (string.IsNullOrEmpty(variant.Sku))
            {
                var product = _productRepo.GetProductById(variant.ProductId);
                variant.Sku = GenerateHelper.GenerateSKU(
                    product?.Name ?? "PROD",
                    variant.Color ?? "DEF",
                    variant.Size ?? "OS"
                );
            }

            // Check SKU uniqueness
            if (_variantRepo.IsSkuExists(variant.Sku))
                return (false, "SKU already exists. Please use a different SKU.", null);

            variant.IsActive = true;
            variant.StockQuantity ??= 0;

            _variantRepo.AddProductVariant(variant);
            return (true, "", variant);
        }

        public (bool Success, string Error) UpdateVariant(ProductVariant variant)
        {
            if (variant.ProductId <= 0 || variant.VariantId <= 0)
                return (false, "Invalid variant or product ID.");

            if (string.IsNullOrEmpty(variant.Sku))
                return (false, "SKU is required.");

            if (variant.Price <= 0)
                return (false, "Price must be greater than 0.");

            var existingVariant = _variantRepo.GetProductVariantBySku(variant.Sku);
            if (existingVariant != null && existingVariant.VariantId != variant.VariantId)
                return (false, "SKU already exists for another variant.");

            variant.StockQuantity ??= 0;
            _variantRepo.UpdateProductVariant(variant);
            return (true, "");
        }

        public (bool Success, string Error) DeleteVariant(int variantId, int productId)
        {
            var variant = _variantRepo.GetProductVariantById(variantId);
            if (variant == null)
                return (false, "Variant not found.");
            if (variant.ProductId != productId)
                return (false, "Variant does not belong to the specified product.");

            _variantRepo.DeleteProductVariant(variantId);
            return (true, "");
        }

        public (bool Success, string Error) SoftDeleteVariant(int variantId, int productId)
        {
            var variant = _variantRepo.GetProductVariantById(variantId);
            if (variant == null)
                return (false, "Variant not found.");
            if (variant.ProductId != productId)
                return (false, "Variant does not belong to the specified product.");

            var success = _variantRepo.SoftDeleteProductVariant(variantId);
            return success ? (true, "") : (false, "Variant not found.");
        }

        public (bool Success, string Error) UpdateStock(int variantId, int newStock)
        {
            var success = _variantRepo.UpdateStockQuantity(variantId, newStock);
            return success ? (true, "") : (false, "Variant not found.");
        }

        public (bool Success, string Error) ToggleStatus(int variantId, bool isActive)
        {
            var success = _variantRepo.ToggleVariantStatus(variantId, isActive);
            return success ? (true, "") : (false, "Variant not found.");
        }

        public bool IsSkuAvailable(string sku, int? excludeVariantId = null)
        {
            var existing = _variantRepo.GetProductVariantBySku(sku);
            return existing == null || (excludeVariantId.HasValue && existing.VariantId == excludeVariantId.Value);
        }
    }
}