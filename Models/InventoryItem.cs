using System.Text.Json.Serialization;

namespace Inventory_Management.Models
{
    public class InventoryItem
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("current_price")] public decimal CurrentPrice { get; set; }
        [JsonPropertyName("stock_quantity")] public int StockQuantity { get; set; }
        [JsonPropertyName("barcode")] public string Barcode { get; set; } = string.Empty;
    }
}

