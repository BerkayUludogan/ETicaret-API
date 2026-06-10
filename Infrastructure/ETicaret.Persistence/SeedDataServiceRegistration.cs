using ETicaret.Application.Common.CustomAttributes;
using ETicaret.Persistence.Context;
using ETicaret.Persistence.Seed.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ETicaret.Persistence
{
    public static class SeedDataServiceRegistration
    {
        public static void SeedDataServices(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ETicaretContext>();

                var seeders = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => typeof(ISeeder).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .Select(type => new
                    {
                        Instance = (ISeeder)Activator.CreateInstance(type)!,
                        Order = type.GetCustomAttribute<SeedOrderAttribute>()?.Order ?? int.MaxValue
                    })
                    .OrderBy(x=>x.Order)
                    .Select(x=>x.Instance);

                foreach (var seeder in seeders)
                {
                    seeder.Seed(context);
                }
            }
        }
    }
}
