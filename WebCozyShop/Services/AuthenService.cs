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
        private readonly ITokenRepository _tokenRepository;
        public AuthenService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public User DoLogin(LoginUserRequest loginRequest)
        {
            var user = _userRepository.GetUserByEmailOrUsername(loginRequest.Username);

            if (user == null || !PasswordHelper.VerifyPassword(loginRequest.Password, user.PasswordHash))
                return new User();

            return user;
        }

        public bool DoResetPassword(string email, string password)
        {
            return _userRepository.ResetPassword(email, PasswordHelper.HashPassword(password));
        }

        public string SaveToken(string email)
        {
            var token = Guid.NewGuid().ToString();
            var expiration = DateTime.UtcNow.AddMinutes(30);
            _tokenRepository.SaveToken(email, token, expiration);

            return token;
        }

        public bool VerifyToken(string email, string token)
        {
            try
            {
                return _tokenRepository.IsTokenValid(email, token);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
