
using ETicaret.Application.Common.Abstractions.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Mapper
{
    public static class ServiceRegistration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
