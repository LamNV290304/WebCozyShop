using WebCozyShop.Helper;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;
using WebCozyShop.Requests;

namespace WebCozyShop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string roleDefault = "staff";
        private readonly CozyShopDbContext _context;

        public UserRepository(CozyShopDbContext context)
        {
            _context = context;
        }

        public bool ChangePassword(int userId, string newPasswordHash)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null) return false;

            user.PasswordHash = newPasswordHash;

            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }

        public int CountPages(string searchTerm)
        {
            var query = _context.Users.AsQueryable().Where(u => !u.Role.ToLower().Contains("admin"));
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = _context.Users
                    .Where(u => !u.Role.ToLower().Contains("admin") && u.FullName != null && u.FullName.ToLower().Contains(searchTerm));
            }

            return query.Count();
        }

        public bool CreateUser(CreateUserRequest user)
        {
            if (user == null) return false;
            var newUser = new User
            {
                FullName = user.FullName,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Dob = user.Dob,
                PasswordHash = PasswordHelper.DefaultPassword(),
                Role = roleDefault
            };

            _context.Users.Add(newUser);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null) return false;
            _context.Users.Remove(user);
            return _context.SaveChanges() > 0;
        }

        public List<User> GetPagedUsers(int pageIndex, int pageSize, string searchTerm)
        {
            pageIndex = Math.Max(0, pageIndex);

            var query = _context.Users.AsQueryable().Where(u => !u.Role.ToLower().Contains("admin"));
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = _context.Users
                    .Where(u => !u.Role.ToLower().Contains("admin") && u.FullName != null && u.FullName.ToLower().Contains(searchTerm));
            }

            return query
                .OrderBy(u => u.FullName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public User? GetUserByEmailOrUsername(string ue)
        {
            return _context.Users
                .FirstOrDefault(u => u.Username.Equals(ue) || u.Email.Equals(ue)) ?? new User();
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserID == userId) ?? new User();
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username.Equals(username)) ?? new User();
        }

        public bool isEmailExists(string email)
        {
            return _context.Users.Any(u => u.Email.Equals(email));
        }

        public bool isPhoneExists(string phone)
        {
            return _context.Users.Any(u => u.Phone.Equals(phone));
        }

        public bool isUsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username.Equals(username));
        }

        public bool ResetPassword(string email, string passwordHash)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.Equals(email));
            if (user == null) return false;

            user.PasswordHash = passwordHash;
            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(UpdateUserRequest user)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.UserID == user.Id);
            if (userToUpdate == null) return false;

            userToUpdate.FullName = user.FullName;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Dob = user.Dob;
            _context.Users.Update(userToUpdate);
            return _context.SaveChanges() > 0;
        }
    }
}
