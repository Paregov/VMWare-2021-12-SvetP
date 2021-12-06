using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WarehouseBackend.DataAccess.Entities;
using WarehouseBackend.DataAccess.Entities.Results;

namespace WarehouseBackend.DataAccess
{
    public class WarehouseDataAccess : IWarehouseDataAccess
    {
        public WarehouseDataAccess(
            WarehouseDbContext context,
            ILogger<WarehouseDataAccess> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ContainerEntity> AddContainerAsync(ContainerEntity container,
            CancellationToken cancellationToken = default)
        {
            // Note: This is a bad practice, but good enough for the demo
            await _context.Database.EnsureCreatedAsync(cancellationToken);

            var result = await _context.Containers.AddAsync(
                container, cancellationToken)
                .ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result.Entity;
        }

        public async Task<ContainerEntity> GetContainerByIdAsync(string id,
            CancellationToken cancellationToken = default)
        {
            // Note: This is a bad practice, but good enough for the demo
            await _context.Database.EnsureCreatedAsync(cancellationToken);

            var result = await _context.Containers
                .Where(c => c.Id.Equals(id))    // This is case sensitive, so in real application need to be changed to case insensitive compare
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<ContainerEntityResults> GetContainersAsync(int page = 0, int pageSize = 50,
            CancellationToken cancellationToken = default)
        {
            // Note: This is a bad practice, but good enough for the demo
            await _context.Database.EnsureCreatedAsync(cancellationToken);

            var results = await _context.Containers
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new ContainerEntityResults()
            {
                Containers = results,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<ContainerEntityResults> GetContainersShipmentByDatesAsync(
            int page, int pageSize,
            DateTime shipmentDateLow,
            DateTime shipmentDateHigh,
            CancellationToken cancellationToken = default)
        {
            // Note: This is a bad practice, but good enough for the demo
            await _context.Database.EnsureCreatedAsync(cancellationToken);

            var results = await _context.Containers
                .Where(c => shipmentDateLow <= c.DateShipped &&
                            shipmentDateHigh >= c.DateShipped)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new ContainerEntityResults()
            {
                Containers = results,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<ContainerEntity> UpdateContainerAsync(
            ContainerEntity container,
            CancellationToken cancellationToken = default)
        {
            // Note: This is a bad practice, but good enough for the demo
            await _context.Database.EnsureCreatedAsync(cancellationToken);

            var result = _context.Containers.Update(container);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result.Entity;
        }

        public async Task DeleteContainerAsync(string id,
            CancellationToken cancellationToken = default)
        {
            var container = await GetContainerByIdAsync(id, cancellationToken).ConfigureAwait(false);

            _context.Containers.Remove(container);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private readonly WarehouseDbContext _context;
        private readonly ILogger<WarehouseDataAccess> _logger;
    }
}
