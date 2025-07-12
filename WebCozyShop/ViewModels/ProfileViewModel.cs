using WebCozyShop.Models;
using WebCozyShop.Requests;

namespace WebCozyShop.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }

        public UpdateUserRequest UpdateRequest { get; set; } = new();
    }
}
