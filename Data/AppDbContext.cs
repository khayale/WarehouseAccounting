using Microsoft.EntityFrameworkCore;

namespace WarehouseAccounting.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
