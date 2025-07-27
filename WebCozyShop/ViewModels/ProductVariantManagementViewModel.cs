using WebCozyShop.Models;

namespace WebCozyShop.ViewModels
{
    public class ProductVariantManagementViewModel
    {
        public Product Product { get; set; } = null!;
        public List<ProductVariant> PagedVariants { get; set; } = new List<ProductVariant>();
        public string Search { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int TotalVariants { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}