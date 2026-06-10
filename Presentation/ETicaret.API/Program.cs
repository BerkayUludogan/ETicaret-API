using ETicaret.API;
using ETicaret.Application;
using ETicaret.Infrastructure;
using ETicaret.Mapper;
using ETicaret.Persistence;
using ETicaret.Redis;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();//Client'tan gelen request neticesinde oluşturulan HttpContext nesnesine katmanlardaki class'lar üzerinden erişebilmemizi sağlar.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AppApi(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCustomMapper();
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddRedisServices(builder.Configuration);

#region Project Environments
var env = builder.Environment;

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
#endregion




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
/// Seed data registration
app.SeedDataServices();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRateLimiter(); // 1 dakikada maksimum 80 istek atılabilir. 80'den fazla istek atıldığında 429 Too Many Requests hatası dönecektir.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
