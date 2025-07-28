using WebCozyShop.Models;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Services.Interface
{
    public interface IProductVariantService
    {
        // Create
        bool AddProductVariant(ProductVariant variant);

        // Read
        ProductVariant? GetProductVariantById(int variantId);
        ProductVariant? GetProductVariantBySku(string sku);
        ProductVariantManagementViewModel GetProductVariantsPaged(int productId, string search, int pageIndex, int pageSize);

        // Update
        bool UpdateProductVariant(ProductVariant variant);
        void ImportProducts(List<ImportItemViewModel> items);
        List<ImportItemViewModel> ValidateImport(List<ImportItemViewModel> items);

        // Delete
        bool DeleteProductVariant(int variantId, int productId);
    }
}