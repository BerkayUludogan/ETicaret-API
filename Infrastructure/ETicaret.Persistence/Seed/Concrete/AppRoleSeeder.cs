//using ETicaret.Application.Common.Enums;
//using ETicaret.Domain.Entities.Identity;
//using ETicaret.Persistence.Seed.Abstract;
//using Microsoft.EntityFrameworkCore;

//namespace ETicaret.Persistence.Seed.Concrete
//{

//    public class AppRoleSeeder : ISeeder
//    {
//        public void Seed(DbContext context)
//        {
//            if (!context.Set<AppRoleEntity>()
//                 .Any(x => x.Name == RoleNames.Admin.ToString() && x.Name == RoleNames.Customer.ToString()))
//            {
//                AppRoleEntity adminRole = new()
//                {
//                    Id = Guid.NewGuid(),
//                    Name = RoleNames.Admin.ToString(),
//                    NormalizedName = "ADMIN",
//                    Description = "Yönetici rolü, tüm sistem yönetim yetkilerine sahip",
//                    IsActive = true,
//                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
//                    CreatedDate = DateTime.Now,
//                };
//                AppRoleEntity customerRole = new()
//                {
//                    Id = Guid.NewGuid(),
//                    Name = RoleNames.Customer.ToString(),
//                    NormalizedName = "CUSTOMER",
//                    Description = "Müşteri",
//                    IsActive = true,
//                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
//                    CreatedDate = DateTime.Now,
//                };



//                context.Set<AppRoleEntity>().AddRange( customerRole);
//                context.SaveChanges();
//            }
//        }
//    }
//}
