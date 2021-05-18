using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace RequestToDomain
{
    public static class RequestToDomainFunction
    {
        [FunctionName(nameof(RequestToDomainFunction))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] UpdateUserWorkRequest updateUserWorkRequest,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var process = new UpdateUserWorkProcess(updateUserWorkRequest.Ssn, updateUserWorkRequest.Work);
                process.Run();

                return new OkResult();
            }
            catch (Exception exception)
            {
                // handle the exeption, do error logging
                log.LogError($"Exception caught: {exception.Message}");

                // and return a proper http status
                return new StatusCodeResult(555);
            }
        }
    }
}