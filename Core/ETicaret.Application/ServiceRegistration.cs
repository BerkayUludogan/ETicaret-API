using ETicaret.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBeavior<,>));
        }
    }
}
