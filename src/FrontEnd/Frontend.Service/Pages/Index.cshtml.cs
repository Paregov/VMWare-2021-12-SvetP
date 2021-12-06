using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend.Service.Services;
using WarehouseBackend.Models;

namespace Frontend.Service.Pages
{
    public class IndexModel : PageModel
    {
        public List<Container> Data { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        
        public bool ShowPrevious => CurrentPage > 1;
        // TODO: Need to have upper bound
        public bool ShowNext => true;

        public IndexModel(
            IWarehouseService warehouseService,
            ILogger<IndexModel> logger)
        {
            _warehouseService = warehouseService;
            _logger = logger;
            // Data = new List<Container>();
        }

        public string FormatDate(DateTime dateTime)
        {
            if (dateTime.CompareTo(DateTime.MinValue.AddMinutes(1)) < 0 ||
                dateTime.CompareTo(DateTime.MaxValue.AddMinutes(-1)) > 0)
                return "---";

            return dateTime.ToString("s");
        }

        public async Task OnGetAsync()
        {
            // Need to subtract 1 from the CurrentPage. Check not 0
            if (CurrentPage <= 0)
            {
                CurrentPage = 1;
            }

            Data = await _warehouseService.GetContainersAsync(CurrentPage - 1, PageSize);
        }

        private readonly IWarehouseService _warehouseService;
        private readonly ILogger<IndexModel> _logger;
    }
}
