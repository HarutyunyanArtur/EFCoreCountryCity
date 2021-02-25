using Microsoft.Extensions.DependencyInjection;

namespace EFCoreCountryCity.Services
{
    public static class ServiceDependency
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<ICityService, CityService>();
            return services;
        }
    }
}
