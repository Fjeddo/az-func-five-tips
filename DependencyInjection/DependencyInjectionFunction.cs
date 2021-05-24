using System.Threading.Tasks;
using DependencyInjection.Domain.Processes;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DependencyInjection
{
    public class DependencyInjectionFunction
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPersonService _personService;
        private readonly SomePersonProcess _somePersonProcess;
        private readonly SomeUtilityClass _someUtilityClass;

        public DependencyInjectionFunction(IHttpContextAccessor httpContextAccessor, IPersonService personService, SomePersonProcess somePersonProcess, SomeUtilityClass someUtilityClass)
        {
            _httpContextAccessor = httpContextAccessor;
            _personService = personService;
            _somePersonProcess = somePersonProcess;
            _someUtilityClass = someUtilityClass;
        }
        
        [FunctionName(nameof(DependencyInjectionFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            return new OkObjectResult(new 
            {
                HttpContextAccessor_is_not_null = _httpContextAccessor != null,
                PersonService_is_not_null = _personService != null,
                SomePersonProcess_is_not_null = _somePersonProcess != null,
                SomeUtilityClass_is_not_null = _someUtilityClass != null,
                Log_is_not_null = log != null
            });
        }
    }
}
