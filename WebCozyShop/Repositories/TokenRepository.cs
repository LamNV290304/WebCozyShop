using Microsoft.EntityFrameworkCore;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;

namespace WebCozyShop.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly CozyShopContext _CozyShopContext;

        public TokenRepository(CozyShopContext CozyShopContext)
        {
            _CozyShopContext = CozyShopContext;
        }

        public bool IsTokenValid(string email, string token)
        {
            var existingToken = _CozyShopContext.Tokens
                .AsNoTracking()
                .FirstOrDefault(t => t.Email == email && t.Token1 == token && t.Status == false && t.ExpiresAt > DateTime.UtcNow);

            if (existingToken == null) return false;

            existingToken.Status = true;
            _CozyShopContext.Update(existingToken);
            _CozyShopContext.SaveChanges();
            return true;
        }
        

        public void SaveToken(string email, string token, DateTime expireTime)
        {
            var existing = _CozyShopContext.Tokens.FirstOrDefault(t => t.Email == email);
            if (existing != null)
            {
                existing.Token1 = token;
                existing.ExpiresAt = expireTime;
                existing.Status = false;
                _CozyShopContext.Update(existing);
            }
            else
            {
                var newToken = new Token
                {
                    Email = email,
                    Token1 = token,
                    ExpiresAt = expireTime,
                    Status = false
                };
                _CozyShopContext.Tokens.Add(newToken);
            }

            _CozyShopContext.SaveChanges();
        }
    }
}
