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
                entity.ToTable("categories", "dbo");

                entity.HasKey(e => e.categ_num);

                entity.Property(e => e.categ_num)
                    .HasColumnName("categ_num")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.categ_name)
                    .HasColumnName("categ_name")
                    .HasMaxLength(255);
            });
        }
    }
}