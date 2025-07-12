using System.ComponentModel.DataAnnotations;

namespace WebCozyShop.Requests
{
    public class ResetPassRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; } = default!;

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = default!;
    }
}
