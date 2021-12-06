using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseBackend.Models;
using WarehouseBackend.Models.ApiRequests;

namespace Frontend.Service.Services
{
    public interface IWarehouseService
    {
        public Task<Container> AddContainerAsync(AddContainerRequest request);

        public Task<List<Container>> GetContainersAsync(int page, int pageSize);
    }
}
