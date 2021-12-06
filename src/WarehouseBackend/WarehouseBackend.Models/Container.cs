using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseBackend.Models
{
    public class Container
    {
        public Container()
        {
            Id = Guid.NewGuid().ToString("N");
            DateBooked = DateTime.Now;
            DateIn = DateTime.MaxValue;
            DateExpectedToShip = DateTime.MinValue;
            DateShipped = DateTime.MaxValue;
            Type = "Standard";
            Notes = string.Empty;
        }

        /// <summary>
        /// Id of the container
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id of the client that owns the container
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Date and time when the container was booked
        /// </summary>
        public DateTime DateBooked { get; set; }

        /// <summary>
        /// Date and time when the container came into the warehouse
        /// </summary>
        public DateTime DateIn { get; set; }

        /// <summary>
        /// Date when it is expected to be shipped
        /// </summary>
        public DateTime DateExpectedToShip { get; set; }

        /// <summary>
        /// Date and time when it was actually shipped
        /// </summary>
        public DateTime DateShipped { get; set; }

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
