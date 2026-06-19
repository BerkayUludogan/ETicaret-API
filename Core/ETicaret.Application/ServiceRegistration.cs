using ETicaret.Application.Common.Behaviors;
using ETicaret.Application.Features.Addresses.Rules;
using ETicaret.Application.Features.Auth.Rules;
using ETicaret.Application.Features.Baskets.Rules;
using ETicaret.Application.Features.Categories.Rules;
using ETicaret.Application.Features.Orders.Rules;
using ETicaret.Application.Features.Payments.Rules;
using ETicaret.Application.Features.Products.Rules;
using ETicaret.Application.Features.Users.Rules;
using FluentValidation;
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

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehaviour<,>));


            services.AddScoped<IUserBusinessRules, UserBusinessRules>();
            services.AddScoped<IAuthBusinessRules, AuthBusinessRules>();
            services.AddScoped<ICategoryBusinessRules, CategoryBusinessRules>();
            services.AddScoped<IProductBusinessRules, ProductBusinessRules>();
            services.AddScoped<IBasketBusinessRules, BasketBusinessRules>();
            services.AddScoped<IOrderBusinessRules, OrderBusinessRules>();
            services.AddScoped<IAddressBusinessRules, AddressBusinessRules>();
            services.AddScoped<IPaymentBusinessRules, PaymentBusinessRules>();

        }
    }
}
