using System;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBackend.Models;
using WarehouseBackend.Models.ApiRequests;
using WarehouseBackend.Models.ApiResponses;

namespace WarehouseBackend.Core.Services
{
    public interface IWarehouseService
    {
        public Task<Container> AddContainerAsync(AddContainerRequest request,
            CancellationToken cancellationToken = default);

        public Task<Container> GetContainerByIdAsync(string id,
            CancellationToken cancellationToken = default);

        public Task<ContainerResults> GetContainersAsync(
            int page = 0, int pageSize = 50,
            CancellationToken cancellationToken = default);

        public Task<ContainerResults> GetContainersShipmentByClientIdAndDatesAsync(
            int page, int pageSize, string clientId,
            DateTime shipmentDateLow, DateTime shipmentDateHigh,
            CancellationToken cancellationToken = default);

        public Task<Container> MarkContainerShippedAsync(string id,
            CancellationToken cancellationToken = default);

        public Task DeleteContainerAsync(string id,
            CancellationToken cancellationToken = default);
    }
}
