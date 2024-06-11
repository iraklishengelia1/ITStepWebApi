using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Data.Configs
{
    public class UserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.UserName).IsUnicode(false).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => new { x.UserName }).IsUnique();
            builder.HasIndex(x => new { x.Email }).IsUnique();
            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //builder.HasData(
            //    new ApplicationUser
            //    {
            //        Firstname = "Irakli",
            //        Lastname = "Shengelia",
            //        EmailConfirmed = true,
            //        PhoneNumber = "555111222",
            //        PhoneNumberConfirmed = true,
            //        UserName = "Admin",
            //        NormalizedUserName = "ADMIN",
            //        PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null!, "Aa123456"),
            //        Email = "Admin@gmail.com",
            //        NormalizedEmail = "ADMIN@GMAIL.COM",
            //        Id = 1,
            //        SecurityStamp = Guid.NewGuid().ToString(),
            //    });
        }
    }
}
