using WebCozyShop.Models;

namespace WebCozyShop.Services.Interface
{
    public interface IProductVariantService
    {
        (bool Success, string Error, ProductVariant? Variant) CreateVariant(ProductVariant variant);
        (bool Success, string Error) UpdateVariant(ProductVariant variant);
        (bool Success, string Error) DeleteVariant(int variantId, int productId);
        (bool Success, string Error) SoftDeleteVariant(int variantId, int productId);
        (bool Success, string Error) UpdateStock(int variantId, int newStock);
        (bool Success, string Error) ToggleStatus(int variantId, bool isActive);
        bool IsSkuAvailable(string sku, int? excludeVariantId = null);
    }
}