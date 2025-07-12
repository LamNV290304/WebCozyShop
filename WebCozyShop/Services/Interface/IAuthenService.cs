using WebCozyShop.Models;
using WebCozyShop.Requests;

namespace WebCozyShop.Services.Interface
{
    public interface IAuthenService
    {
        User DoLogin(LoginUserRequest loginRequest);
        string SaveToken(string email);
        bool VerifyToken(string email, string token);
        bool DoResetPassword(string email, string password);
    }
}
