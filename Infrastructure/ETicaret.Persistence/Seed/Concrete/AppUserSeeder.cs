using ETicaret.Application.Common.CustomAttributes;
using ETicaret.Application.Common.Enums;
using ETicaret.Domain.Entities.Identity;
using ETicaret.Persistence.Seed.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Seed.Concrete
{
    [SeedOrder(2)]
    public class AppUserSeeder : ISeeder
    {
        public void Seed(DbContext context)
        {
            var adminUser = context.Set<AppUserEntity>()
                .FirstOrDefault(x => x.NormalizedEmail == "ADMIN@GMAIL.COM");

            if (adminUser is null)
            {
                var hasher = new PasswordHasher<AppUserEntity>();

                adminUser = new AppUserEntity
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                };

                adminUser.PasswordHash = hasher.HashPassword(adminUser, "123456");
                context.Set<AppUserEntity>().Add(adminUser);
                context.SaveChanges();
            }

            var adminRole = context.Set<AppRoleEntity>()
                .FirstOrDefault(x => x.NormalizedName == RoleNames.Admin);

            if (adminRole is null)
                return;

            var userHasAdminRole = context.Set<IdentityUserRole<Guid>>()
                .Any(x => x.UserId == adminUser.Id && x.RoleId == adminRole.Id);

            if (userHasAdminRole)
                return;

            context.Set<IdentityUserRole<Guid>>().Add(new IdentityUserRole<Guid>
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            });

            context.SaveChanges();
        }
    }
}
