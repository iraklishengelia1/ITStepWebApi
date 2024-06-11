using Microsoft.AspNetCore.Identity;

namespace WebApi.Models.Users
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; } = string.Empty;
    }
    public enum Roles : byte
    {
        Admin,
        Consumer,
    }
}
