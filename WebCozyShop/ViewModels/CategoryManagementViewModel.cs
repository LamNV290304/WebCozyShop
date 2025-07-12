using System.ComponentModel.DataAnnotations;
using WebCozyShop.Models;

namespace WebCozyShop.ViewModels
{
    public class CategoryManagementViewModel
    {
        public List<Category> Categories { get; set; } = new();

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty; 
    }
}
