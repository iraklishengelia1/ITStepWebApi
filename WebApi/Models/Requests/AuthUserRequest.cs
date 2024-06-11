using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class AuthUserRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
