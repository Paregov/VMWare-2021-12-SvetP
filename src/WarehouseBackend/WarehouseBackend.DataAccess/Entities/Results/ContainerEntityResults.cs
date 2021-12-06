using System.Collections.Generic;

namespace WarehouseBackend.DataAccess.Entities.Results
{
    public class ContainerEntityResults
    {
        public List<ContainerEntity> Containers { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
