using System.Collections.Generic;
using System.Threading.Tasks;
using ResourcesApi.Models;

namespace ResourcesApi.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Resource>> GetResourcesAsync();
    }
}