using ETicaret.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ETicaret.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(assembly));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehaviour<,>));
        }
    }
}
