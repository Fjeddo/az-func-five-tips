using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RequestInterception
{
    public class InterceptingBaseFunction
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<InterceptingBaseFunction> _log;

        protected InterceptingBaseFunction(IHttpContextAccessor httpContextAccessor, ILogger<InterceptingBaseFunction> log)
        {
            _httpContextAccessor = httpContextAccessor;
            _log = log;
        }

        protected async Task<IActionResult> Execute(Func<Task<IActionResult>> func)
        {
            try
            {
                var requestCookie = _httpContextAccessor.HttpContext.Request.Cookies["required-cookie"];
                if (requestCookie == null)
                {
                    return new BadRequestResult();
                }

                var result = await func();

                _log.LogInformation("This is after the function executed");

                return result;
            }
            catch (Exception e)
            {
                _log.LogError("Exception caught", e);
                throw;
            }
        }
    }
}