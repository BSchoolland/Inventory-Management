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

                // Use case-insensitive collation for Name to enable indexed searches
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .UseCollation("NOCASE");

                entity.Property(e => e.Description)
                    .HasMaxLength(1024);

                entity.Property(e => e.CurrentPrice)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Barcode)
                    .HasMaxLength(128);

                // Index on Name for fast text searches (NOCASE collation enables case-insensitive index scans)
                entity.HasIndex(e => e.Name)
                    .HasDatabaseName("IX_Items_Name");

                // Index on CurrentPrice for price filtering
                entity.HasIndex(e => e.CurrentPrice)
                    .HasDatabaseName("IX_Items_CurrentPrice");

                // Index on StockQuantity for stock filtering
                entity.HasIndex(e => e.StockQuantity)
                    .HasDatabaseName("IX_Items_StockQuantity");

                // Composite index for common filter combinations (price + stock)
                entity.HasIndex(e => new { e.CurrentPrice, e.StockQuantity })
                    .HasDatabaseName("IX_Items_Price_Stock");

                // Create unique index on barcode, but only for non-empty values
                entity.HasIndex(e => e.Barcode)
                    .IsUnique()
                    .HasFilter("[Barcode] != ''");
            });
        }
    }
}

