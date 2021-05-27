using DependencyInjection.Domain.Processes;
using DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public static class DomainIoCConfiguration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddTransient<SomePersonProcess>();

            return services;
        }
    }
}