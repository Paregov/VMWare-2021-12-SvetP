using System.Collections.Generic;

namespace WarehouseBackend.Models.ApiResponses
{
    public class ContainerResults
    {
        public List<Container> Containers { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
