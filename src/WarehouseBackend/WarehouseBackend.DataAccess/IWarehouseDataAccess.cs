using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WarehouseBackend.DataAccess.Entities;
using WarehouseBackend.DataAccess.Entities.Results;

namespace WarehouseBackend.DataAccess
{
    public interface IWarehouseDataAccess
    {
        public Task<ContainerEntity> AddContainerAsync(ContainerEntity container,
            CancellationToken cancellationToken = default);

        public Task<ContainerEntity> GetContainerByIdAsync(string id,
            CancellationToken cancellationToken = default);

        public Task<ContainerEntityResults> GetContainersAsync(int page = 0, int pageSize = 50,
            CancellationToken cancellationToken = default);

        public Task<ContainerEntityResults> GetContainersShipmentByDatesAsync(int page, int pageSize,
            DateTime shipmentDateLow, DateTime shipmentDateHigh,
            CancellationToken cancellationToken = default);

        public Task<ContainerEntity> UpdateContainerAsync(ContainerEntity container,
            CancellationToken cancellationToken = default);

        public Task DeleteContainerAsync(string id,
            CancellationToken cancellationToken = default);
    }
}
