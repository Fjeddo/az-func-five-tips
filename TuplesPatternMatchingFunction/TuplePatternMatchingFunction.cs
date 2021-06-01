using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TuplesPatternMatchingFunction.Domain.Processes;

namespace TuplesPatternMatchingFunction
{
public static class TuplePatternMatchingFunction
{
    [FunctionName(nameof(TuplePatternMatchingFunction))]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] UpdateUserWorkRequest updateUserWorkRequest,
        ILogger log)
    {
        var process = new UpdateUserWorkProcess(updateUserWorkRequest.Ssn, updateUserWorkRequest.Work);
        var (success, model, status) = process.Run();

        // Do some mapping of status to proper http status
        return (success, status) switch
        {
            (true, _) => new OkObjectResult(model),
            (false, -1) => new NotFoundResult(),
            (false, -999) => new StatusCodeResult(400),

            _ => new InternalServerErrorResult()
        };
    }
}
}
