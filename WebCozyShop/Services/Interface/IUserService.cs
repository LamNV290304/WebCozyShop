using WebCozyShop.Models;

namespace WebCozyShop.Services.Interface
{
    public interface IUserService
    {
        User? GetUserById(int userId);

        User? GetUserByEmail(string email);

    }
}
