using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services.Interface;
using WebCozyShop.Helper;
using WebCozyShop.ViewModels;

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

        public bool AddProductVariant(ProductVariant variant)
        {

            var product = _productRepo.GetProductById(variant.ProductId);
            string sku = GenerateHelper.GenerateSKU(product.Name, variant.Color, variant.Size);

            if (_variantRepo.GetProductVariantBySku(sku) != null)
            {
                throw new ArgumentException("SKU must be unique.");
            }

            variant.Sku = sku;
            variant.IsActive = true;
            _variantRepo.AddProductVariant(variant);
            return true;

        }

        public bool DeleteProductVariant(int variantId, int productId)
        {
            var variant = _variantRepo.GetProductVariantById(variantId);
            if (variant == null) return false;
            if (variant.ProductId != productId) return false;

            _variantRepo.DeleteProductVariant(variantId);
            return true;
        }

        public ProductVariant? GetProductVariantById(int variantId)
        {
            return _variantRepo.GetProductVariantById(variantId);
        }

        public ProductVariant? GetProductVariantBySku(string sku)
        {
            return _variantRepo.GetProductVariantBySku(sku);
        }

        public ProductVariantManagementViewModel GetProductVariantsPaged(int productId, string search, int pageIndex, int pageSize)
        {
            try
            {
                Product product = _productRepo.GetProductById(productId);

                List<ProductVariant> productVariants = _variantRepo.GetProductVariantsPaged(productId, search, pageIndex, pageSize);
                int totalVariants = _variantRepo.CountProductVariants(productId, search);
                int totalPages = (int)Math.Ceiling((double)totalVariants / pageSize);

                var viewModel = new ProductVariantManagementViewModel
                {
                    Product = product,
                    PagedVariants = productVariants,
                    Search = search,
                    CurrentPage = pageIndex,
                    PageSize = pageSize,
                    TotalVariants = totalVariants,
                    TotalPages = totalPages
                };

                return viewModel;
            }
            catch
            {
                return null;
            }
        }

        public void ImportProducts(List<ImportItemViewModel> items)
        {
            foreach (var item in items)
            {
                var variant = _variantRepo.GetProductVariantBySku(item.Sku);
                if (variant != null)
                {
                    variant.StockQuantity += item.QuantityToImport;
                    _variantRepo.UpdateStockQuantity(variant.VariantId, variant.StockQuantity);
                    item.Error = null;
                }
                else
                {
                    item.Error = $"SKU '{item.Sku}' not found!";
                }
            }
        }

        public List<ImportItemViewModel> ValidateImport(List<ImportItemViewModel> items)
        {
            foreach (var item in items)
            {
                var variant = _variantRepo.GetProductVariantBySku(item.Sku);
                if (variant == null)
                {
                    item.Error = $"SKU '{item.Sku}' not found.";
                }
            }

            return items;
        }

        public bool UpdateProductVariant(ProductVariant variant)
        {
            try
            {
                _variantRepo.UpdateProductVariant(variant);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}