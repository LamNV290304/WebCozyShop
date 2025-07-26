using WebCozyShop.Helper;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Requests;
using WebCozyShop.Services.Interface;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? GetUserById(int UserId)
        {
            return _userRepository.GetUserById(UserId);
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

        public UserListViewModel GetUsersPaged(string seachTerm, int pageNumber, int pageSize)
        {
            seachTerm = ValidationHelper.NormalizeSearchTerm(seachTerm);

            var total = _userRepository.CountPages(seachTerm);
            var users = _userRepository.GetPagedUsers(pageNumber, pageSize, seachTerm);

            return new UserListViewModel
            {
                Users = users,
                SearchTerm = seachTerm,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)total / pageSize)
            };
        }

        public bool DeleteUser(int UserId)
        {
            return _userRepository.DeleteUser(UserId) && UserId != 1;
        }

        public bool CreateUser(CreateUserRequest user)
        {
            if (_userRepository.isUsernameExists(user.Username) ||
                _userRepository.isEmailExists(user.Email) ||
                _userRepository.isPhoneExists(user.Phone))
            {
                return false;
            }

            return _userRepository.CreateUser(user);
        }
    }
}
