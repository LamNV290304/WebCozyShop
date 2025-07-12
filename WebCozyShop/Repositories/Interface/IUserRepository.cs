using WebCozyShop.Models;

namespace WebCozyShop.Repositories.Interface
{
    public interface IUserRepository
    {
        User? GetUserById(int userId);
        User? GetUserByEmailOrUsername(string ue);
        User? GetUserByUsername(string username);
        bool ResetPassword(string email, string passwordHash);
    }
}
