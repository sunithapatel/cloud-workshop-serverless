using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ResourcesApi.Services;

namespace ResourcesApi.Functions
{
    public class GetResources
    {
        private readonly ICosmosDbService _cosmosDbService;

        public GetResources(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [FunctionName("resources")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var resources = await _cosmosDbService.GetResourcesAsync();

            return new OkObjectResult(resources);
        }
    }
}
