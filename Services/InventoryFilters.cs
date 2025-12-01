using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    public static class InventoryFilters
    {
        public static List<InventoryItem> Apply(
            IEnumerable<InventoryItem> source,
            string search,
            decimal? priceMin,
            decimal? priceMax,
            int? stockMin,
            int? stockMax)
        {
            string s = (search ?? string.Empty).Trim().ToLower();
            decimal pMin = priceMin ?? decimal.MinValue;
            decimal pMax = priceMax ?? decimal.MaxValue;
            int qMin = stockMin ?? int.MinValue;
            int qMax = stockMax ?? int.MaxValue;

            return source
                .Where(item => (string.IsNullOrEmpty(s) || item.Name.ToLower().Contains(s))
                    && (priceMin == null && priceMax == null || item.CurrentPrice >= pMin && item.CurrentPrice <= pMax)
                    && (stockMin == null && stockMax == null || item.StockQuantity >= qMin && item.StockQuantity <= qMax))
                .ToList();
        }
    }
}

