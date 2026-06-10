using ETicaret.Domain.Entities.Identity;
using ETicaret.Persistence.Seed.Abstract;
using Microsoft.EntityFrameworkCore;

//namespace ETicaret.Persistence.Seed.Concrete
//{
   
//    public class AppRoleSeeder : ISeeder
//    {
//        public void Seed(DbContext context)
//        {
//            if (!context.Set<AppRoleEntity>().Any())
//            {
//                AppRoleEntity adminRole = new()
//                {
//                    Id = Guid.Parse("407CC2FE-D098-41E6-8AE3-D36962F0B004"),
//                    Name = RoleNames.Admin.ToString(),
//                    NormalizedName = RoleNames.Admin.ToString(),
//                    Description = "Yönetici rolü, tüm sistem yönetim yetkilerine sahip",
//                    IsActive = true,
//                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
//                    CreatedDate = DateTime.Now,
//                };
//                context.Set<AppRoleEntity>().Add(adminRole);
//                context.SaveChanges();
//            }
//        }
//    }
//}
