
using System.Threading;
using System.Threading.Tasks;
using WarehouseBackend.Models;
using WarehouseBackend.Models.ApiRequests;
using WarehouseBackend.Models.ApiResponses;

namespace WarehouseBackend.ApiClient
{
    public interface IWarehouseClient
    {
        Task<Container> AddContainerAsync(
            AddContainerRequest request,
            CancellationToken cancellationToken = default);

        Task<Container> GetContainerByIdAsync(
            string id, CancellationToken cancellationToken = default);
        
        public  Task<Container> MarkContainerShippedAsync(
            string id, CancellationToken cancellationToken = default);
        
        public Task<ContainerResults> GetContainersAsync(
            GetContainersRequest request, CancellationToken cancellationToken = default);

        public Task<ContainerResults> GetContainersByClientIdAndShipmentDatesAsync(
            GetContainersByClientAndShippingDatesRequest request,
            CancellationToken cancellationToken = default);
        
        public Task DeleteContainerAsync(
            string id, CancellationToken cancellationToken = default);
    }
}
