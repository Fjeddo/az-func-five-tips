using DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace DependencyInjection
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Examples
            // 
            // Add logging (see host.json, and please note https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection#ilogger-and-iloggerfactory)
            // Add http handler/service
            // Add command and query handlers (see CQS blog posts https://techblogg.infozone.se/blog/cqs-plus-functional-eq-true-1_2/, https://techblogg.infozone.se/blog/cqs-plus-functional-eq-true-2_2/)
            // etc
        }
    }
}