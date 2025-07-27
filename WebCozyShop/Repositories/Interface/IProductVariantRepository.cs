using WebCozyShop.Models;

namespace WebCozyShop.Repositories.Interface
{
    public interface IProductVariantRepository
    {
        // Create
        void AddProductVariant(ProductVariant variant);
        
        // Read
        ProductVariant? GetProductVariantById(int variantId);
        List<ProductVariant> GetProductVariantsByProductId(int productId);
        List<ProductVariant> GetAllProductVariants();
        List<ProductVariant> GetActiveProductVariants();
        List<ProductVariant> GetProductVariantsPaged(int productId, int pageIndex, int pageSize);
        int CountProductVariants(int productId);
        
        // Update
        void UpdateProductVariant(ProductVariant variant);
        bool UpdateStockQuantity(int variantId, int newQuantity);
        bool ToggleVariantStatus(int variantId, bool isActive);
        
        // Delete
        void DeleteProductVariant(int variantId);
        bool SoftDeleteProductVariant(int variantId); // Set IsActive to false
        
        // Utility methods
        bool IsVariantExists(int variantId);
        bool IsSkuExists(string sku);
        ProductVariant? GetProductVariantBySku(string sku);
    }
}
