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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories", "storesdata");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("categ_num")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("categ_name")
                    .HasMaxLength(255);
            });
        }
    }
}
