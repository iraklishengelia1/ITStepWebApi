using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class RegisterUserRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Firstname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Lastname { get; set; }
    }
}
