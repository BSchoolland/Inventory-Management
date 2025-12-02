using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.Models
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1024)]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }

        [JsonPropertyName("stock_quantity")]
        public int StockQuantity { get; set; }

        [MaxLength(128)]
        [JsonPropertyName("barcode")]
        public string Barcode { get; set; } = string.Empty;
    }
}

