using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

// Define Inventory class
namespace Inventory_Management.Models
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }
        // Name of item
        [Required]
        [MaxLength(256)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        // Description of item
        [MaxLength(1024)]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        // Price of item (decimal)
        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }
        // How many of this item we have
        [JsonPropertyName("stock_quantity")]
        public int StockQuantity { get; set; }
        // Barcode (unused for now)
        [MaxLength(128)]
        [JsonPropertyName("barcode")]
        public string Barcode { get; set; } = string.Empty;
    }
}

