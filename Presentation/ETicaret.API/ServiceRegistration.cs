using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

namespace ETicaret.API
{
    public static class ServiceRegistration
    {
        public static void AppApi(this IServiceCollection services, IConfiguration configuration)
        {
            #region Swagger
            services.AddSwaggerGen(gen =>
            {
                var webUrl = "http://OnionArcTemp.com";
                gen.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ETicaret Web Api",
                    Version = "v1",
                    License = new OpenApiLicense
                    {
                        Name = "Powered by Berkay Uludoğan",
                        Url = new Uri(webUrl),
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "Berkay Uludoğan",
                        Email = "buludogan0@gmail.com"
                    }
                });

                gen.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization: Bearer {token}"
                });
                gen.AddSecurityRequirement(doc =>

                    new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("Bearer", doc)] = new List<string>()
                    });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new()
                    {
                        ValidateAudience = true, // Oluşturulacak token değerinin kimlerin/hangi originlerin/sitelerin kullanıcı belirlediğimiz değerdir.
                        ValidateIssuer = true, // Oluşturulacak token değerinin kimin dağıttığını ifade edeceğimiz alan
                        ValidateLifetime = true, // Oluşturulan token değerinin süresini kontrol edecek olan doğrulama
                        ValidateIssuerSigningKey = true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden suciry key verisinin doğrulamasıdır.

                        ValidAudience = configuration["JWT:Audience"],
                        ValidIssuer = configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"])),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                        expires != null ? expires > DateTime.UtcNow : false,
                        RoleClaimType = ClaimTypes.Role,
                        NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz.
                    };
                });

            #endregion
            #region Rate Limiting
            services.AddRateLimiter(opt =>
            {
                opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                opt.AddFixedWindowLimiter("fixed", limiterOpt =>
                {
                    limiterOpt.PermitLimit = 80; //80 Request
                    limiterOpt.Window = TimeSpan.FromMinutes(1);
                    limiterOpt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOpt.QueueLimit = 0;
                });
            });
            #endregion
        }
    }
}
