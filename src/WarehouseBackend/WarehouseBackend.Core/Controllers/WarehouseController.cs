using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WarehouseBackend.Core.Services;
using WarehouseBackend.Models;
using WarehouseBackend.Models.ApiRequests;
using WarehouseBackend.Models.ApiResponses;

namespace WarehouseBackend.Core.Controllers
{
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        public WarehouseController(
            ILogger<WarehouseController> logger,
            IWarehouseService warehouseService)
        {
            _logger = logger;
            _warehouseService = warehouseService;
        }

        [HttpGet("/")]
        public ActionResult<string> GetRoot()
        {
            return "Hello from the WarehouseController!";
        }

        [HttpPost("api/containers")]
        public async Task<ActionResult<Container>> AddContainerAsync(
            [FromBody]AddContainerRequest request,
            CancellationToken cancellationToken = default)
        {
            return await _warehouseService
                .AddContainerAsync(request, cancellationToken)
                .ConfigureAwait(false);
        }

        [HttpGet("api/containers/{id}")]
        public async Task<ActionResult<Container>> GetContainerByIdAsync(
            [FromRoute]string id, CancellationToken cancellationToken = default)
        {
            return await _warehouseService
                .GetContainerByIdAsync(id, cancellationToken)
                .ConfigureAwait(false);
        }

        [HttpPut("api/containers/{id}")]
        public async Task<ActionResult<Container>> MarkContainerShippedAsync(
            [FromRoute] string id, CancellationToken cancellationToken = default)
        {
            return await _warehouseService
                .MarkContainerShippedAsync(id, cancellationToken)
                .ConfigureAwait(false);
        }

        [HttpGet("api/containers")]
        public async Task<ActionResult<ContainerResults>> GetContainersAsync(
            [FromQuery] GetContainersRequest request, CancellationToken cancellationToken = default)
        {
            return await _warehouseService
                .GetContainersAsync(request.Page, request.PageSize, cancellationToken)
                .ConfigureAwait(false);
        }

        [HttpGet("api/containers/shipped")]
        public async Task<ActionResult<ContainerResults>> GetContainersByClientIdAndShipmentDatesAsync(
            [FromQuery] GetContainersByClientAndShippingDatesRequest request,
            CancellationToken cancellationToken = default)
        {
            return await _warehouseService
                .GetContainersShipmentByClientIdAndDatesAsync(
                    request.Page, request.PageSize,
                    request.ClientId,
                    request.ShipmentDateLow, 
                    request.ShipmentDateHigh,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        [HttpDelete("api/containers/{id}")]
        public async Task DeleteContainerAsync(
            [FromRoute] string id, CancellationToken cancellationToken = default)
        {
            await _warehouseService
                .DeleteContainerAsync(id, cancellationToken)
                .ConfigureAwait(false);
        }
        
        private readonly ILogger<WarehouseController> _logger;
        private readonly IWarehouseService _warehouseService;
    }
}
