
using Microsoft.AspNetCore.Identity;

namespace WebApi.Models.Users
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Firstname { get;  set; }
        public string Lastname { get; set; }
        
    }
}
