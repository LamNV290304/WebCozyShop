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
        int CountPages(string searchTerm);
        List<User> GetPagedUsers(int pageIndex, int pageSize, string searchTerm);
        bool DeleteUser(int userId);
        bool CreateUser(CreateUserRequest user);
        bool isUsernameExists(string username);
        bool isEmailExists(string email);
        bool isPhoneExists(string phone);
    }
}
