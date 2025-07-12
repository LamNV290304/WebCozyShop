using WebCozyShop.Models;
using WebCozyShop.Requests;

namespace WebCozyShop.Services.Interface
{
    public interface IUserService
    {
        User? GetUserById(int userId);

        User? GetUserByEmail(string email);

        string UpdateUser(UpdateUserRequest request);
        bool ChangePassword(int id,ChangePasswordRequest request);

    }
}
