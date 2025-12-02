using Microsoft.EntityFrameworkCore;
using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    /// <summary>
    /// Entity Framework Core database context for inventory management.
    /// </summary>
    public class InventoryDbContext : DbContext
    {
        public DbSet<InventoryItem> Items { get; set; } = null!;

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure InventoryItem entity
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Description)
                    .HasMaxLength(1024);

                entity.Property(e => e.CurrentPrice)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Barcode)
                    .HasMaxLength(128);

                // Create unique index on barcode, but only for non-empty values
                entity.HasIndex(e => e.Barcode)
                    .IsUnique()
                    .HasFilter("[Barcode] != ''");
            });
        }
    }
}

