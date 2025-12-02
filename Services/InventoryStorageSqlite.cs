using Microsoft.EntityFrameworkCore;
using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    /// <summary>
    /// SQLite-based storage service for inventory items.
    /// Handles database initialization and CRUD operations.
    /// </summary>
    public static class InventoryStorageSqlite
    {
        private static InventoryDbContext? _dbContext;

        /// <summary>
        /// Initializes the database context and ensures the database exists.
        /// </summary>
        public static void Initialize()
        {
            var dbPath = GetDatabasePath();
            var folder = Path.GetDirectoryName(dbPath);
            
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            _dbContext = new InventoryDbContext(options);

            // Ensure database is created with the current model
            _dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Gets the database file path.
        /// </summary>
        public static string GetDatabasePath()
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "data");
            return Path.Combine(dir, "inventory.db");
        }

        /// <summary>
        /// Gets the current database context.
        /// </summary>
        private static InventoryDbContext GetContext()
        {
            if (_dbContext == null)
            {
                throw new InvalidOperationException("Database context has not been initialized. Call Initialize() first.");
            }
            return _dbContext;
        }

        /// <summary>
        /// Loads all items from the database.
        /// </summary>
        public static List<InventoryItem> LoadItems()
        {
            try
            {
                var context = GetContext();
                return context.Items.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load items from database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Saves all items to the database, replacing existing data.
        /// </summary>
        public static void SaveItems(List<InventoryItem> items)
        {
            try
            {
                var context = GetContext();
                
                // Clear existing items
                context.Items.RemoveRange(context.Items);
                context.SaveChanges();

                // Add new items
                context.Items.AddRange(items);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save items to database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Adds a single item to the database.
        /// </summary>
        public static void AddItem(InventoryItem item)
        {
            try
            {
                var context = GetContext();
                context.Items.Add(item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add item to database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates an existing item in the database.
        /// </summary>
        public static void UpdateItem(InventoryItem item)
        {
            try
            {
                var context = GetContext();
                context.Items.Update(item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update item in database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        public static void DeleteItem(InventoryItem item)
        {
            try
            {
                var context = GetContext();
                context.Items.Remove(item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete item from database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Disposes the database context.
        /// </summary>
        public static void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}

