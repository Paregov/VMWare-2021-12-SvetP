using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseBackend.Models.ApiRequests
{
    public class AddContainerRequest
    {
        /// <summary>
        /// Id of the client that owns the container
        /// </summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// Date when it is expected to be shipped
        /// </summary>
        public DateTime DateExpectedToShip { get; set; }

        /// <summary>
        /// Type of the shipment
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Notes fot the container
        /// </summary>
        public string Notes { get; set; }
    }
}
