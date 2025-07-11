using WebCozyShop.Models;
using WebCozyShop.Requests;

namespace WebCozyShop.Services.Interface
{
    public interface IAuthenService
    {
        User DoLogin(LoginUserRequest loginRequest);
    }
}
