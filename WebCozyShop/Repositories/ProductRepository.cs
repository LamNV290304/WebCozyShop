using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace WebCozyShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CozyShopContext _CozyShopContext;

        public ProductRepository(CozyShopContext CozyShopContext)
        {
            _CozyShopContext = CozyShopContext;
        }

        public void AddProduct(Product product)
        {
            try
            {
                product.CreatedAt = DateTime.UtcNow;
                _CozyShopContext.Products.Add(product);
                _CozyShopContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }

        public int Count(string search, int CategoryId)
        {
            try
            {
                var query = _CozyShopContext.Products.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(p => p.Name.Contains(search));

                if (CategoryId > 0)
                    query = query.Where(p => p.CategoryId == CategoryId);

                return query.Count();
            }
            catch (Exception ex)
            {
                throw new Exception("Error counting products.", ex);
            }
        }

        public void DeleteProduct(int ProductId)
        {
            try
            {
                var product = _CozyShopContext.Products.Find(ProductId);
                if (product != null)
                {
                    _CozyShopContext.Products.Remove(product);
                    _CozyShopContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }

        public Product GetProductById(int ProductId)
        {
            try
            {
                return _CozyShopContext.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductVariants)
                    .FirstOrDefault(p => p.ProductId == ProductId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product by ID.", ex);
            }
        }

        public List<Product> GetProductsPaged(string search, int CategoryId, int pageIndex, int pageSize)
        {
            try
            {
                var query = _CozyShopContext.Products
                    .Include(p => p.Category)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(p => p.Name.Contains(search));

                if (CategoryId > 0)
                    query = query.Where(p => p.CategoryId == CategoryId);

                return query
                    .OrderBy(p => p.ProductId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving paged products.", ex);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                _CozyShopContext.Products.Update(product);
                _CozyShopContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }
    }
}
