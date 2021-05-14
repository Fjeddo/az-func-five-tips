using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RequestInterception
{
    public class RequestInterceptionFunction : IFunctionInvocationFilter
    {
        [FunctionName(nameof(RequestInterceptionFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] Person personReq,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var name = personReq.Name;
            var age = personReq.Age;

            return new OkObjectResult(new { name, age });
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
