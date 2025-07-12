using WebCozyShop.Models;
using WebCozyShop.Requests;

namespace WebCozyShop.Repositories.Interface
{
    public interface IUserRepository
    {
        User? GetUserById(int userId);
        User? GetUserByEmailOrUsername(string ue);
        User? GetUserByUsername(string username);
        bool ResetPassword(string email, string passwordHash);
        bool UpdateUser(UpdateUserRequest user);
        bool ChangePassword(int userId, string newPasswordHash);
    }
}
