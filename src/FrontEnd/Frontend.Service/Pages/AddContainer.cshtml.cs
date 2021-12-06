using System;
using System.Threading.Tasks;
using Frontend.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WarehouseBackend.Models.ApiRequests;

namespace Frontend.Service.Pages
{
    public class AddContainerModel : PageModel
    {
        public AddContainerModel(
            IWarehouseService warehouseService,
            ILogger<AddContainerModel> logger)
        {
            _warehouseService = warehouseService;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(
            string clientId,
            DateTime dateExpectedToShip,
            string type, string notes)
        {
            try
            {
                var request = new AddContainerRequest
                {
                    ClientId = clientId,
                    DateExpectedToShip = dateExpectedToShip,
                    Type = type,
                    Notes = notes
                };

                await _warehouseService.AddContainerAsync(request)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding new container.");
                return RedirectToPage("Error");
            }

            _logger.LogInformation("New container added.");
            return RedirectToPage("Index");
        }

        private readonly IWarehouseService _warehouseService;
        private readonly ILogger<AddContainerModel> _logger;
    }
}
