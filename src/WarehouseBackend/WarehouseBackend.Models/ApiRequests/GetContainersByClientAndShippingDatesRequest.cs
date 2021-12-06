using System;

namespace WarehouseBackend.Models.ApiRequests
{
    public class GetContainersByClientAndShippingDatesRequest
    {
        public GetContainersByClientAndShippingDatesRequest()
        {
            ClientId = string.Empty;
            ShipmentDateLow = DateTime.MinValue;
            ShipmentDateHigh = DateTime.Now.AddDays(1);
            Page = 0;
            PageSize = 50;
        }

        public string ClientId { get; set; }
        
        public DateTime ShipmentDateLow { get; set; }

        public DateTime ShipmentDateHigh { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
