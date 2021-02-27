using Microsoft.Extensions.DependencyInjection;

namespace EFCoreCountryCity.Services
{
    public static class ServiceDependency
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICountryService, CountryService>();
            return services;
        }
    }
}
