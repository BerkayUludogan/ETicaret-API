using ETicaret.Domain.Entities.Identity;
using ETicaret.Persistence.Context; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ETicaret.Persistence.UnitOfWorks; 
using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Application.Common.Abstractions.Services;
using ETicaret.Application.Features.Auth.DTOs;
using ETicaret.Persistence.Services;
namespace ETicaret.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ETicaretContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUserEntity, AppRoleEntity>(opt =>
            {
                opt.Password.RequiredLength = AppUserFieldLengths.PasswordMin;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ETicaretContext>().AddDefaultTokenProviders();



         //   services.AddScoped<IUserService, UserService>(); 

            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IAuthAuditService, UserLoginAuditService>();
        }
    }
}
