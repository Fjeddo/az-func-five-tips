using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RequestInterception
{
    public class AnotherInterceptingFunction : InterceptingBaseFunction
    {
        public AnotherInterceptingFunction(IHttpContextAccessor httpContextAccessor, ILogger<InterceptingBaseFunction> log) : base(httpContextAccessor, log)
        { }
        
        [FunctionName("AnotherInterceptingFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            return await Execute(async () =>
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                return new OkObjectResult(new {success = true, data = DateTimeOffset.UtcNow});
            });
        }
    }
}
