using WebCozyShop.Models;

namespace WebCozyShop.ViewModels
{
    public class UserListViewModel
    {
        public List<User> Users { get; set; } = new();
        public string SearchTerm { get; set; } = "";
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
