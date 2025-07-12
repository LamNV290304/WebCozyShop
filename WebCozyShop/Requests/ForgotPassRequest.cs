using System.ComponentModel.DataAnnotations;

namespace WebCozyShop.Requests
{
    public class ForgotPassRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
