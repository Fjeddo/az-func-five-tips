using DependencyInjection;
using DependencyInjection.Domain.Processes;
using DependencyInjection.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace DependencyInjection
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Examples, built in services
            builder.Services.AddLogging();
            builder.Services.AddHttpContextAccessor();

            // Examples function implementation specific services
            builder.Services.AddTransient<IPersonService, PersonService>();
            // Interfaces are not required
            builder.Services.AddScoped<SomePersonProcess>();
            builder.Services.AddSingleton<SomeUtilityClass>();
        }
    }
}