using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Models;

namespace WarehouseAccounting.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
