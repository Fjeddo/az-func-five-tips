using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ModelBinding
{
    public static class ModelBindingFunction
    {
        [FunctionName(nameof(ModelBindingFunction))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] Person personReq,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var name = personReq.Name;
            var age = personReq.Age;

            return new OkObjectResult(new {name, age});
        }
    }
}