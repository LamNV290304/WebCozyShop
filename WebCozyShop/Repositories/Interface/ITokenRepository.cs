namespace WebCozyShop.Repositories.Interface
{
    public interface ITokenRepository
    {
        void SaveToken(string email, string token, DateTime expireTime);
        bool IsTokenValid(string email, string token);
    }
}
