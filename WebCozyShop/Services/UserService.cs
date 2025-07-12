using WebCozyShop.Helper;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Requests;
using WebCozyShop.Services.Interface;

namespace WebCozyShop.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public User? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmailOrUsername(email);
        }

        public string UpdateUser(UpdateUserRequest request)
        {
            try
            {
                bool isSuccess = _userRepository.UpdateUser(request);
                if (isSuccess)
                {
                    return "User updated successfully";
                }
                else
                {
                    return "Failed to update user";
                }
            }
            catch (Exception ex)
            {
                return $"Error updating user";
            }
        }

        public bool ChangePassword(int id,ChangePasswordRequest request)
        {
            User user = _userRepository.GetUserById(id)!;
            
            if (!PasswordHelper.VerifyPassword(request.CurrentPassword, user.PasswordHash))
            {
                return false;
            }

            return _userRepository.ChangePassword(id, PasswordHelper.HashPassword(request.NewPassword));
        }
    }
}
