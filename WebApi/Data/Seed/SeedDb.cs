using Microsoft.AspNetCore.Identity;
using WebApi.Data.Context;
using WebApi.Migrations;
using WebApi.Models.Users;

namespace WebApi.Data.Seed
{
    public class SeedDb
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                SeedAdminAsync(userManager);
            }

        }
        private static void SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = userManager.FindByEmailAsync("Admin@gmail.com").Result;
            _= userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString()).Result;
        }
    }
}
