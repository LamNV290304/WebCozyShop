using WebCozyShop.Models;

namespace WebCozyShop.ViewModels
{
    public class ProductManagementViewModel
    {
        public List<Product> PagedProducts { get; set; }
        public List<Category> Categories { get; set; }
        public string Search { get; set; }
        public string SelectedCategoryId { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
