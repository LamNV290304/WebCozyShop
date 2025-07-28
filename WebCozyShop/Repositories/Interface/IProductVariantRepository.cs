using WebCozyShop.Models;

namespace WebCozyShop.Repositories.Interface
{
    public interface IProductVariantRepository
    {
        // Create
        void AddProductVariant(ProductVariant variant);
        
        // Read
        ProductVariant? GetProductVariantById(int variantId);
        ProductVariant? GetProductVariantBySku(string sku);
        List<ProductVariant> GetProductVariantsByProductId(int productId);
        List<ProductVariant> GetProductVariantsPaged(int productId, string search, int pageIndex, int pageSize);
        int CountProductVariants(int productId, string search);
        
        // Update
        void UpdateProductVariant(ProductVariant variant);
        bool UpdateStockQuantity(int variantId, int? newQuantity);
        
        // Delete
        void DeleteProductVariant(int variantId);
        
    }
}
