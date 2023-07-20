using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JosephKhaipi.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles (User, Admin, SuperAdmin)
            var adminRoldId = "c8124a59-32e8-4d23-8dbc-7146150adfc8";
            var superAdminRoleId = "f23a0b92-d2af-4781-aef0-86e32fd93857";
            var userRoleId = "0b914e04-9e11-438b-b4d1-8494bee5324f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoldId,
                    ConcurrencyStamp = adminRoldId

                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


            //Seed SuperAdminUser
            var superAdminId = "730717a6-89f8-41c8-9ba3-32b2d48242b3";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@josephkhaipi.com",
                Email = "superadmin@josephkhaipi.com",
                NormalizedEmail = "superadmin@josephkhaipi.com".ToUpper(),
                NormalizedUserName = "superadmin@josephkhaipi.com".ToUpper(),
                Id = superAdminId,

            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All roles to SuperAdminUser 
            var superAdminRole = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoldId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRole);
        }
    }
}
