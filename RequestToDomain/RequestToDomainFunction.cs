using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RequestToDomain.Domain.Processes;

namespace RequestToDomain
{
    public static class RequestToDomainFunction
    {
        [FunctionName(nameof(RequestToDomainFunction))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] UpdateUserWorkRequest updateUserWorkRequest,
            ILogger log)
        {
            // "Leave the incoming request behind" as soon as possible, get into the domain instead
            var process = new UpdateUserWorkProcess(updateUserWorkRequest.Ssn, updateUserWorkRequest.Work);
            var (success, model, status) = process.Run();

            if (!success)
            {
                // Do some mapping of status to proper http status
                var httpStatus = status == -1 ? StatusCodes.Status404NotFound : StatusCodes.Status500InternalServerError;

                return new StatusCodeResult(httpStatus);
            }

            return new OkObjectResult(model);
        }
    }
}