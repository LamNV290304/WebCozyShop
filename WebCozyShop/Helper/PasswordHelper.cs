namespace WebCozyShop.Helper
{
    public class PasswordHelper
    {
        public static string HashPassword(string rawPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(rawPassword);
        }

        public static bool VerifyPassword(string rawPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword);
        }

        public static string DefaultPassword()
        {
            return HashPassword("123abc@A"); // Default password
        }
    }
}
