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
            builder.Services.AddSingleton<ICosmosDbService>(s => InitializeCosmosClientInstanceAsync(s.GetService<IConfiguration>().GetSection("CosmosDb")));
        }

        private ICosmosDbService InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;

            var cosmosClient = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            CosmosDbService cosmosDbService = new CosmosDbService(cosmosClient, databaseName, containerName);

            return cosmosDbService;
        }
    }
}