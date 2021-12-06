using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using WarehouseBackend.Core.Exceptions;
using WarehouseBackend.DataAccess;
using WarehouseBackend.DataAccess.Entities;
using WarehouseBackend.Models;
using WarehouseBackend.Models.ApiRequests;
using WarehouseBackend.Models.ApiResponses;

namespace WarehouseBackend.Core.Services
{
    public class WarehouseService : IWarehouseService
    {
        public WarehouseService(
            IWarehouseDataAccess dataAccess,
            IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<Container> AddContainerAsync(AddContainerRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _dataAccess
                .AddContainerAsync(_mapper.Map<ContainerEntity>(request), cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<Container>(result);
        }

        public async Task<Container> GetContainerByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await _dataAccess
                .GetContainerByIdAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<Container>(result);
        }

        public async Task<ContainerResults> GetContainersAsync(int page = 0, int pageSize = 50,
            CancellationToken cancellationToken = default)
        {
            var results = await _dataAccess
                .GetContainersAsync(page, pageSize, cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<ContainerResults>(results);
        }

        public async Task<ContainerResults> GetContainersShipmentByClientIdAndDatesAsync(
            int page, int pageSize, string clientId,
            DateTime shipmentDateLow, DateTime shipmentDateHigh,
            CancellationToken cancellationToken = default)
        {
            var results = await _dataAccess
                .GetContainersShipmentByDatesAsync(page, pageSize, shipmentDateLow, shipmentDateHigh, cancellationToken)
                .ConfigureAwait(false);

            if (!string.IsNullOrEmpty(clientId))
                results.Containers = results.Containers.Where(c => c.ClientId.Equals(clientId)).ToList();

            return _mapper.Map<ContainerResults>(results);
        }

        public async Task<Container> MarkContainerShippedAsync(string id, CancellationToken cancellationToken = default)
        {
            var container = await _dataAccess
                .GetContainerByIdAsync(id, cancellationToken)
                .ConfigureAwait(false);

            if (container == null)
                throw new WarehouseBackendException($"No container found with id: {id}");

            container.DateShipped = DateTime.Now;

            var result = await _dataAccess
                .UpdateContainerAsync(container, cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<Container>(result);
        }

        public async Task DeleteContainerAsync(string id, CancellationToken cancellationToken = default)
        {
            await _dataAccess
                .DeleteContainerAsync(id, cancellationToken)
                .ConfigureAwait(false);
        }

        private readonly IWarehouseDataAccess _dataAccess;
        private readonly IMapper _mapper;
    }
}
