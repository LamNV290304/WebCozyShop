using Microsoft.AspNetCore.Identity.Data;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Services.Interface;
using WebCozyShop.Requests;
using WebCozyShop.Helper;

namespace WebCozyShop.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly IUserRepository _userRepository;

        public AuthenService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
        }

        public User DoLogin(LoginUserRequest loginRequest)
        {
            var user = _userRepository.GetUserByEmailOrUsername(loginRequest.Username);

            if (user == null || !PasswordHelper.VerifyPassword(loginRequest.Password, user.PasswordHash))
                return new User();

            return user;
        }
    }
}
