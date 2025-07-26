using WebCozyShop.Models;
using WebCozyShop.Requests;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Services.Interface
{
    public interface IUserService
    {
        User? GetUserById(int UserId);
        User? GetUserByEmail(string email);
        string UpdateUser(UpdateUserRequest request);
        bool ChangePassword(int id,ChangePasswordRequest request);
        UserListViewModel GetUsersPaged(string seachTerm ,int pageNumber, int pageSize);
        bool DeleteUser(int UserId);
        bool CreateUser(CreateUserRequest user);
    }
}
