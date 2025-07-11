using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;

namespace WebCozyShop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CozyShopDbContext _context;

        public UserRepository(CozyShopDbContext context)
        {
            _context = context;
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
    }
}
