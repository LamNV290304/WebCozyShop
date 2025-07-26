using WebCozyShop.Models;

namespace WebCozyShop.Repositories.Interface
{
    public interface IProductRepository
    {
        List<Product> GetProductsPaged(string search,int CategoryId, int pageIndex, int pageSize);
        int Count(string search, int CategoryId);
        Product GetProductById(int ProductId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int ProductId);
    }
}
