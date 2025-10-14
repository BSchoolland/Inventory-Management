using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory_Management
{
    public static class InventoryStorage
    {
        public static string GetDataFilePath()
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "data");
            return Path.Combine(dir, "items.json");
        }

        public static void EnsureDataFile()
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, "items.json");
            if (!File.Exists(path)) File.WriteAllText(path, "[ { \"items\": [] } ]");
        }

        public static List<InventoryItem> LoadItems()
        {
            EnsureDataFile();
            try
            {
                string json = File.ReadAllText(GetDataFilePath());
                var roots = JsonSerializer.Deserialize<List<StorageRoot>>(json) ?? new List<StorageRoot>();
                var items = roots.Count > 0 ? roots[0].Items : new List<InventoryItem>();
                return items;
            }
            catch
            {
                return new List<InventoryItem>();
            }
        }

        public static void SaveItems(List<InventoryItem> items)
        {
            var roots = new List<StorageRoot> { new StorageRoot { Items = items } };
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(roots, options);
            File.WriteAllText(GetDataFilePath(), json);
        }

        private class StorageRoot
        {
            [JsonPropertyName("items")] public List<InventoryItem> Items { get; set; } = new();
        }
    }

    public class InventoryItem
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("current_price")] public decimal CurrentPrice { get; set; }
        [JsonPropertyName("stock_quantity")] public int StockQuantity { get; set; }
        [JsonPropertyName("barcode")] public string Barcode { get; set; } = string.Empty;
    }
}


