using Microsoft.EntityFrameworkCore;
using WebCozyShop.Models;
using WebCozyShop.Repositories.Interface;

namespace WebCozyShop.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly CozyShopDbContext _cozyShopDbContext;

        public TokenRepository(CozyShopDbContext cozyShopDbContext)
        {
            _cozyShopDbContext = cozyShopDbContext;
        }

        public bool IsTokenValid(string email, string token)
        {
            var existingToken = _cozyShopDbContext.Tokens
                .AsNoTracking()
                .FirstOrDefault(t => t.Email == email && t.Token1 == token && t.Status == false && t.ExpiresAt > DateTime.UtcNow);

            if (existingToken == null) return false;

            existingToken.Status = true;
            _cozyShopDbContext.Update(existingToken);
            _cozyShopDbContext.SaveChanges();
            return true;
        }
        

        public void SaveToken(string email, string token, DateTime expireTime)
        {
            var existing = _cozyShopDbContext.Tokens.FirstOrDefault(t => t.Email == email);
            if (existing != null)
            {
                existing.Token1 = token;
                existing.ExpiresAt = expireTime;
                existing.Status = false;
                _cozyShopDbContext.Update(existing);
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
                _cozyShopDbContext.Tokens.Add(newToken);
            }

            _cozyShopDbContext.SaveChanges();
        }
    }
}
