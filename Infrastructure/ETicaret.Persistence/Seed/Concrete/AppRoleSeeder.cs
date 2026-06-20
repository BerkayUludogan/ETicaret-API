using ETicaret.Application.Common.CustomAttributes;
using ETicaret.Application.Common.Enums;
using ETicaret.Domain.Entities.Identity;
using ETicaret.Persistence.Seed.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Seed.Concrete
{
    [SeedOrder(1)]
    public class AppRoleSeeder : ISeeder
    {
        public void Seed(DbContext context)
        {
            SeedRole(context, RoleNames.Admin, "Admin role with system management permissions");
            SeedRole(context, RoleNames.Customer, "Customer role");

            context.SaveChanges();
        }

        private static void SeedRole(DbContext context, string roleName, string description)
        {
            var roleExists = context.Set<AppRoleEntity>()
                .Any(x => x.NormalizedName == roleName);

            if (roleExists)
                return;

            var role = new AppRoleEntity
            {
                Id = Guid.NewGuid(),
                Name = roleName,
                NormalizedName = roleName,
                Description = description,
                IsActive = true,
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                CreatedDate = DateTime.UtcNow
            };

            context.Set<AppRoleEntity>().Add(role);
        }
    }
}
