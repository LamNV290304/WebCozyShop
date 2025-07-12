using System.ComponentModel.DataAnnotations;

namespace WebCozyShop.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Dob { get; set; }
    }
}
