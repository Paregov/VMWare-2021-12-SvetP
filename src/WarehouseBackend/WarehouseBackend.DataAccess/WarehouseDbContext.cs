using Microsoft.EntityFrameworkCore;
using WarehouseBackend.DataAccess.Entities;

namespace WarehouseBackend.DataAccess
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<ContainerEntity> Containers { get; set; }
    }
}
