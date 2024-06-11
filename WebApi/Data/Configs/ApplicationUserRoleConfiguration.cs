using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models.Users;

namespace WebApi.Data.Configs
{
    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        private const int adminId = 1;
        private const int consumerId = 2;
 
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData
               (new ApplicationRole
               {
                   Id = adminId,
                   Description = "Administrator have All permisions!",
                   Name = Roles.Admin.ToString(),
                   NormalizedName = Roles.Admin.ToString().ToUpper(),
                   ConcurrencyStamp = DateTime.Now.ToString(),
               }, new ApplicationRole
               {
                   Id = consumerId,
                   Description = "Consumers have  permisions To buy a book",
                   Name = Roles.Consumer.ToString(),
                   NormalizedName = Roles.Consumer.ToString().ToUpper(),
                   ConcurrencyStamp = DateTime.Now.ToString(),
               });
        }
    }
}
