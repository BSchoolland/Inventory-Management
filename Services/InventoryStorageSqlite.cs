using Microsoft.EntityFrameworkCore;
using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    /// <summary>
    /// Snapshot of high-level inventory statistics for the Overview page.
    /// </summary>
    public class InventoryStats
    {
        public int TotalItems { get; set; }
        public long TotalUnitsInStock { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal AverageStockPerItem { get; set; }
        public decimal TotalInventoryValue { get; set; }
    }

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
        /// Gets the total count of items in the database.
        /// </summary>
        public static int GetTotalItemCount()
        {
            try
            {
                var context = GetContext();
                return context.Items.Count();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get item count: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Computes high-level statistics about the inventory.
        /// Uses database-side aggregation so it stays fast even for large datasets.
        /// </summary>
        public static InventoryStats GetInventoryStats()
        {
            try
            {
                var context = GetContext();
                var query = context.Items.AsQueryable();

                var totalItems = query.Count();
                if (totalItems == 0)
                {
                    // Return an empty snapshot so callers don't have to special-case null.
                    return new InventoryStats();
                }

                var totalUnitsInStock = query.Sum(i => (long)i.StockQuantity);
                // SQLite provider does not support aggregate functions over decimal directly; cast to double.
                var averagePriceDouble = query.Average(i => (double)i.CurrentPrice);
                var minPriceDouble = query.Min(i => (double)i.CurrentPrice);
                var maxPriceDouble = query.Max(i => (double)i.CurrentPrice);

                // Compute total inventory value (Price * Quantity) using server-side aggregation.
                // Cast to double to keep SQLite/EF happy with decimal math, then cast back.
                var totalInventoryValueDouble = query
                    .Select(i => (double)i.CurrentPrice * i.StockQuantity)
                    .Sum();

                var averageStockPerItem = totalItems == 0
                    ? 0
                    : (decimal)totalUnitsInStock / totalItems;

                return new InventoryStats
                {
                    TotalItems = totalItems,
                    TotalUnitsInStock = totalUnitsInStock,
                    AveragePrice = (decimal)averagePriceDouble,
                    MinPrice = (decimal)minPriceDouble,
                    MaxPrice = (decimal)maxPriceDouble,
                    AverageStockPerItem = decimal.Round(averageStockPerItem, 2),
                    TotalInventoryValue = (decimal)totalInventoryValueDouble
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get inventory statistics: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Loads items with pagination support.
        /// </summary>
        /// <param name="pageNumber">1-based page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>List of items for the specified page</returns>
        public static List<InventoryItem> LoadItemsPaged(int pageNumber, int pageSize = 100)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 100;

                var context = GetContext();
                int skip = (pageNumber - 1) * pageSize;
                
                return context.Items
                    .OrderBy(i => i.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load paginated items: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Searches items by name across the entire database and returns paginated results.
        /// </summary>
        /// <param name="searchTerm">Search term to match against item names</param>
        /// <param name="pageNumber">1-based page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="totalCount">Output: Total count of matching items</param>
        /// <returns>List of matching items for the specified page</returns>
        public static List<InventoryItem> SearchItems(string searchTerm, int pageNumber, int pageSize, out int totalCount)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 100;

                var context = GetContext();
                
                // Search across entire database - using ToLower for EF Core translation
                string searchLower = searchTerm.ToLower();
                var query = context.Items
                    .Where(i => i.Name.ToLower().Contains(searchLower));

                totalCount = query.Count();
                
                int skip = (pageNumber - 1) * pageSize;
                
                return query
                    .OrderBy(i => i.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to search items: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Filters items by criteria across the entire database and returns paginated results.
        /// </summary>
        /// <param name="minPrice">Minimum price filter (null for no limit)</param>
        /// <param name="maxPrice">Maximum price filter (null for no limit)</param>
        /// <param name="minStock">Minimum stock quantity filter (null for no limit)</param>
        /// <param name="maxStock">Maximum stock quantity filter (null for no limit)</param>
        /// <param name="pageNumber">1-based page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="totalCount">Output: Total count of matching items</param>
        /// <returns>List of matching items for the specified page</returns>
        public static List<InventoryItem> FilterItems(decimal? minPrice, decimal? maxPrice, int? minStock, int? maxStock, int pageNumber, int pageSize, out int totalCount)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 100;

                var context = GetContext();

                var query = context.Items.AsQueryable();

                if (minPrice.HasValue)
                    query = query.Where(i => i.CurrentPrice >= minPrice.Value);

                if (maxPrice.HasValue)
                    query = query.Where(i => i.CurrentPrice <= maxPrice.Value);

                if (minStock.HasValue)
                    query = query.Where(i => i.StockQuantity >= minStock.Value);

                if (maxStock.HasValue)
                    query = query.Where(i => i.StockQuantity <= maxStock.Value);

                totalCount = query.Count();

                int skip = (pageNumber - 1) * pageSize;

                return query
                    .OrderBy(i => i.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to filter items: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets all items by name for suggestions/autocomplete (searches entire database).
        /// </summary>
        /// <param name="searchTerm">Search term to match</param>
        /// <returns>List of matching item names</returns>
        public static List<string> GetItemNamesBySearchTerm(string searchTerm, int limit = 5)
        {
            try
            {
                var context = GetContext();
                
                string searchLower = searchTerm.ToLower();
                return context.Items
                    .Where(i => i.Name.ToLower().Contains(searchLower))
                    .Select(i => i.Name)
                    .Distinct()
                    .OrderBy(n => n)
                    .Take(limit)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get item names: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Bulk adds multiple items to the database efficiently using batching.
        /// </summary>
        /// <param name="items">Items to add</param>
        /// <param name="batchSize">Number of items per batch (default 5000)</param>
        public static int BulkAddItems(List<InventoryItem> items, int batchSize = 5000)
        {
            if (items == null || items.Count == 0)
                return 0;

            try
            {
                var context = GetContext();
                int inserted = 0;

                // Process items in batches
                for (int i = 0; i < items.Count; i += batchSize)
                {
                    var batch = items.Skip(i).Take(batchSize).ToList();
                    context.Items.AddRange(batch);
                    context.SaveChanges();
                    inserted += batch.Count;
                }

                return inserted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to bulk add items to database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Bulk updates multiple items in the database efficiently using batching.
        /// </summary>
        /// <param name="items">Items to update</param>
        /// <param name="batchSize">Number of items per batch (default 5000)</param>
        public static int BulkUpdateItems(List<InventoryItem> items, int batchSize = 5000)
        {
            if (items == null || items.Count == 0)
                return 0;

            try
            {
                var context = GetContext();
                int updated = 0;

                // Process items in batches
                for (int i = 0; i < items.Count; i += batchSize)
                {
                    var batch = items.Skip(i).Take(batchSize).ToList();
                    foreach (var item in batch)
                    {
                        context.Items.Update(item);
                    }
                    context.SaveChanges();
                    updated += batch.Count;
                }

                return updated;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to bulk update items in database: {ex.Message}", ex);
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

