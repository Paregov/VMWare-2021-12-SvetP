using System;

namespace WarehouseBackend.DataAccess.Entities
{
    public class ContainerEntity
    {
        public ContainerEntity()
        {
            Id = Guid.NewGuid().ToString("N");
            DateBooked = DateTime.Now;
            DateIn = DateTime.MaxValue;
            DateExpectedToShip = DateTime.MinValue;
            DateShipped = DateTime.MaxValue;
            Type = "Standard";
            Notes = string.Empty;
        }

        public string Id { get; set; }

        public string ClientId { get; set; }

        public DateTime DateBooked { get; set; }

        public DateTime DateIn { get; set; }

        public DateTime DateExpectedToShip { get; set; }

        public DateTime DateShipped { get; set; }

        public string Type { get; set; }

        public string Notes { get; set; }
    }
}
