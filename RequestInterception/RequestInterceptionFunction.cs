using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using RequestInterception.Exceptions;

namespace RequestInterception
{
    public class RequestInterceptionFunction : IFunctionInvocationFilter, IFunctionExceptionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RequestInterceptionFunction> _log;

        public RequestInterceptionFunction(IHttpContextAccessor httpContextAccessor, ILogger<RequestInterceptionFunction> log)
        {
            _httpContextAccessor = httpContextAccessor;
            _log = log;
        }
        
        [FunctionName(nameof(RequestInterceptionFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] Person personReq,
            HttpRequest request)
        {
            _log.LogTrace("C# HTTP trigger function processed a request");

            var name = personReq.Name;
            var age = personReq.Age;

            return new OkObjectResult(new { name, age });
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            _log.LogTrace("In OnExecutingAsync");
                
            // do interception "on the way in"
            //
            if (executingContext.Arguments.TryGetValue("request", out var request))
            {
                ValidateHttpRequest(request as HttpRequest);
            }

            if (executingContext.Arguments.TryGetValue("personReq", out var obj))
            {
                try
                {
                    ValidatePersonRequest(obj as Person);
                }
                catch (Exception e)
                {
                    return Task.FromException(e);
                }
            }

            return Task.CompletedTask;
        }

        private void ValidatePersonRequest(Person person)
        {
            // do validation on person request object
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                throw new PropertyMissingException("name");
            }

            if (person.Age < 18)
            {
                throw new PersonTooYoungException();
            }
        }

        private void ValidateHttpRequest(HttpRequest request)
        {
            // do validation on http request, cookies, http context etc are available
            var cookies = request.Cookies;
            var httpContext = request.HttpContext;
        }

        public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            _log.LogTrace("In OnExecutedAsync");

            // do interception "on the way out"
            //
            return Task.CompletedTask;
        }

        public async Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
        {
            _log.LogTrace("In OnExceptionAsync");

            if (exceptionContext.Exception.InnerException is PropertyValidationFailedException)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await _httpContextAccessor.HttpContext.Response.WriteAsync(exceptionContext.Exception.InnerException.Message, cancellationToken);
            }
        }
    }
}
