using WebCozyShop.Helper;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public int Count(string search, int CategoryId)
        {
            return _productRepository.Count(search, CategoryId);
        }

        public void DeleteProduct(int ProductId)
        {
            _productRepository.DeleteProduct(ProductId);
        }

        public Product GetProductById(int ProductId)
        {
            return _productRepository.GetProductById(ProductId);
        }

        public ProductManagementViewModel GetProductsPaged(string search, int CategoryId, int pageIndex, int pageSize)
        {
            search = ValidationHelper.NormalizeSearchTerm(search);
            var products = _productRepository.GetProductsPaged(search, CategoryId, pageIndex, pageSize);
            var totalCount = _productRepository.Count(search, CategoryId);
            var categories = _categoryRepository.GetAllCategories();

            return new ProductManagementViewModel
            {
                PagedProducts = products,
                Categories = categories,
                Search = search,
                SelectedCategoryId = CategoryId.ToString(),
                CurrentPage = pageIndex,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }


        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }
    }
}
