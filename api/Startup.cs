using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourcesApi.Services;

[assembly: FunctionsStartup(typeof(ResourcesApi.Startup))]

namespace ResourcesApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ICosmosDbService>(s => InitializeCosmosClientInstance(s.GetService<IConfiguration>()));
        }

        private ICosmosDbService InitializeCosmosClientInstance(IConfiguration? configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }
            
            string databaseName = configuration["CosmosDbDatabaseName"];
            string containerName = configuration["CosmosDbContainerName"];
            string account = configuration["CosmosDbAccount"];
            string key = configuration["CosmosDbKey"];

            var cosmosClient = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            CosmosDbService cosmosDbService = new CosmosDbService(cosmosClient, databaseName, containerName);

            return cosmosDbService;
        }
    }
}