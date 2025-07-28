using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace WebCozyShop.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly CozyShopContext _CozyShopContext;

        public ProductVariantRepository(CozyShopContext CozyShopContext)
        {
            _CozyShopContext = CozyShopContext;
        }

        public void AddProductVariant(ProductVariant variant)
        {
            try
            {
                _CozyShopContext.ProductVariants.Add(variant);
                _CozyShopContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the product variant.", ex);
            }
        }

        public ProductVariant? GetProductVariantById(int variantId)
        {
            try
            {
                return _CozyShopContext.ProductVariants
                    .Include(pv => pv.Product)
                    .FirstOrDefault(pv => pv.VariantId == variantId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product variant by ID.", ex);
            }
        }

        public List<ProductVariant> GetProductVariantsByProductId(int productId)
        {
            try
            {
                return _CozyShopContext.ProductVariants
                    .Where(pv => pv.ProductId == productId)
                    .Include(pv => pv.Product)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product variants by product ID.", ex);
            }
        }

        public List<ProductVariant> GetProductVariantsPaged(int productId, string search, int pageIndex, int pageSize)
        {
            try
            {
                var query = _CozyShopContext.ProductVariants
                    .Include(pv => pv.Product)
                    .Where(pv => pv.ProductId == productId);

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(pv =>
                        (pv.Sku != null && pv.Sku.Contains(search)) ||
                        (pv.Color != null && pv.Color.Contains(search)) ||
                        (pv.Size != null && pv.Size.Contains(search)));
                }

                return query
                    .OrderByDescending(pv => pv.VariantId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving paged product variants.", ex);
            }
        }


        public int CountProductVariants(int productId, string search)
        {
            try
            {
                var query = _CozyShopContext.ProductVariants
                    .Where(pv => pv.ProductId == productId);

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(pv =>
                        (pv.Sku != null && pv.Sku.Contains(search)) ||
                        (pv.Color != null && pv.Color.Contains(search)) ||
                        (pv.Size != null && pv.Size.Contains(search)));
                }

                return query.Count();
            }
            catch (Exception ex)
            {
                throw new Exception("Error counting product variants.", ex);
            }
        }


        public void UpdateProductVariant(ProductVariant variant)
        {
            try
            {
                var tracked = _CozyShopContext.ProductVariants
                    .FirstOrDefault(v => v.VariantId == variant.VariantId);

                if (tracked == null)
                    throw new Exception("Product variant not found.");

                tracked.Sku = variant.Sku;
                tracked.Color = variant.Color;
                tracked.Size = variant.Size;
                tracked.Price = variant.Price;
                tracked.StockQuantity = variant.StockQuantity;
                tracked.ImageUrl ??= !string.IsNullOrWhiteSpace(variant.ImageUrl) ? variant.ImageUrl : tracked.ImageUrl;
                tracked.IsActive = variant.IsActive;
                tracked.ProductId = variant.ProductId;

                _CozyShopContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the product variant.", ex);
            }
        }

        public bool UpdateStockQuantity(int variantId, int newQuantity)
        {
            try
            {
                var variant = _CozyShopContext.ProductVariants.Find(variantId);
                if (variant != null)
                {
                    variant.StockQuantity = newQuantity;
                    _CozyShopContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating stock quantity.", ex);
            }
        }

        public void DeleteProductVariant(int variantId)
        {
            try
            {
                var variant = _CozyShopContext.ProductVariants.Find(variantId);
                if (variant != null)
                {
                    _CozyShopContext.ProductVariants.Remove(variant);
                    _CozyShopContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product variant.", ex);
            }
        }

        public bool IsVariantExists(int variantId)
        {
            try
            {
                return _CozyShopContext.ProductVariants.Any(pv => pv.VariantId == variantId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking if variant exists.", ex);
            }
        }

        

        public ProductVariant? GetProductVariantBySku(string sku)
        {
            try
            {
                return _CozyShopContext.ProductVariants
                    .Include(pv => pv.Product)
                    .FirstOrDefault(pv => pv.Sku == sku);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product variant by SKU.", ex);
            }
        }
    }
}