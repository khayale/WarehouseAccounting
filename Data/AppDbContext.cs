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

                entity.Property(e => e.price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.qntt)
                    .HasColumnName("qntt")
                    .HasMaxLength(255);

                entity.Property(e => e.qnty)
                    .HasColumnName("qnty")
                    .HasMaxLength(255);

                entity.Property(e => e.qntytype)
                    .HasColumnName("qntytype")
                    .HasMaxLength(255);

                entity.Property(e => e.size)
                    .HasColumnName("size")
                    .HasMaxLength(255);

                entity.Property(e => e.color)
                    .HasColumnName("color")
                    .HasMaxLength(255);

                entity.Property(e => e.weight)
                    .HasColumnName("weight")
                    .HasMaxLength(255);

                entity.Property(e => e.model)
                    .HasColumnName("model")
                    .HasMaxLength(255);

                entity.Property(e => e.company)
                    .HasColumnName("company")
                    .HasMaxLength(255);

                entity.Property(e => e.production_date)
                    .HasColumnName("production_date");

                entity.Property(e => e.expiry_date)
                    .HasColumnName("expiry_date");

                entity.Property(e => e.group_name)
                    .HasColumnName("group_name")
                    .HasMaxLength(255);

                entity.Property(e => e.group_num)
                    .HasColumnName("group_num");
            });
        }
    }
}