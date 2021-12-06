
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WarehouseBackend.ApiClient;
using WarehouseBackend.Models;
using WarehouseBackend.Models.ApiRequests;

namespace Frontend.Service.Services
{
    public class WarehouseService : IWarehouseService
    {
        public WarehouseService(
            IWarehouseClient warehouseClient,
            ILogger<WarehouseService> logger)
        {
            _warehouseClient = warehouseClient;
            _logger = logger;
        }

        public async Task<Container> AddContainerAsync(AddContainerRequest request)
        {
            return await _warehouseClient.AddContainerAsync(request).ConfigureAwait(false);
        }

        public async Task<List<Container>> GetContainersAsync(int page, int pageSize)
        {
            var results = await _warehouseClient.GetContainersAsync(
                new GetContainersRequest
            {
                Page = page,
                PageSize = pageSize
            }).ConfigureAwait(false);

            return results.Containers;
        }

        private readonly IWarehouseClient _warehouseClient;
        private readonly ILogger<WarehouseService> _logger;
    }
}
