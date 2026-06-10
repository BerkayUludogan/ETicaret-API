using ETicaret.Application.Common.Enums;
using ETicaret.Domain.Entities.Identity;
using ETicaret.Persistence.Seed.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Seed.Concrete
{
    public class AppUserSeeder : ISeeder
    {
        public void Seed(DbContext context)
        {
            if (!context.Set<AppUserEntity>().Any())
            {
                var hasher = new PasswordHasher<AppUserEntity>();

                AppUserEntity user = new()
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    NormalizedUserName = RoleNames.Admin.ToString(),
                    Email= "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    CreatedDate = DateTime.UtcNow,
                }; 
                user.PasswordHash = hasher.HashPassword(user, "123456");
                context.Set<AppUserEntity>().Add(user);
                context.SaveChanges();
            }
        }
    }
}
