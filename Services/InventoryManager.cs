using System.ComponentModel;
using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    /// <summary>
    /// Centralizes inventory management operations with binding support for UI controls.
    /// Maintains a master List<InventoryItem> that syncs with storage.
    /// </summary>
    public class InventoryManager : INotifyPropertyChanged
    {
        private List<InventoryItem> _masterInventory = new();
        private BindingSource _bindingSource = new();

        public event PropertyChangedEventHandler? PropertyChanged { add { } remove { } }

        public InventoryManager()
        {
            _bindingSource.DataSource = _masterInventory;
            LoadInventory();
        }

        /// <summary>
        /// Gets the BindingSource for DataGridView binding.
        /// </summary>
        public BindingSource BindingSource => _bindingSource;

        /// <summary>
        /// Gets the master inventory list.
        /// </summary>
        public List<InventoryItem> MasterInventory => _masterInventory;

        /// <summary>
        /// Loads inventory from storage into the master list.
        /// </summary>
        public void LoadInventory()
        {
            try
            {
                _masterInventory = InventoryStorageSqlite.LoadItems();
                _bindingSource.DataSource = new BindingSource(_masterInventory, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load inventory: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Adds a new inventory item with validation.
        /// </summary>
        public bool TryAddItem(string name, string description, decimal price, int quantity, string barcode, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                errorMsg = "Item name is required.";
                return false;
            }

            if (price < 0)
            {
                errorMsg = "Price cannot be negative.";
                return false;
            }

            if (quantity < 0)
            {
                errorMsg = "Quantity cannot be negative.";
                return false;
            }

            // Check if item already exists
            if (_masterInventory.Any(i => string.Equals(i.Name, name.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                errorMsg = $"An item named '{name}' already exists.";
                return false;
            }

            var item = new InventoryItem
            {
                Name = name.Trim(),
                Description = description?.Trim() ?? string.Empty,
                CurrentPrice = price,
                StockQuantity = quantity,
                Barcode = barcode?.Trim() ?? string.Empty
            };

            _masterInventory.Add(item);
            _bindingSource.Add(item);
            PersistInventory();
            return true;
        }

        /// <summary>
        /// Increments stock quantity for an item by the specified amount.
        /// </summary>
        public bool TryIncrementStock(int rowIndex, int amount, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (rowIndex < 0 || rowIndex >= _masterInventory.Count)
            {
                errorMsg = "Invalid row selection.";
                return false;
            }

            var item = _masterInventory[rowIndex];
            if (item.StockQuantity + amount < 0)
            {
                errorMsg = $"Cannot decrease stock. '{item.Name}' would drop below 0.";
                return false;
            }

            item.StockQuantity += amount;
            _bindingSource.ResetBindings(false);
            PersistInventory();
            return true;
        }

        /// <summary>
        /// Persists the master inventory to storage.
        /// </summary>
        private void PersistInventory()
        {
            try
            {
                InventoryStorageSqlite.SaveItems(_masterInventory);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save inventory: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refreshes the master inventory and rebinds the UI.
        /// </summary>
        public void Refresh()
        {
            LoadInventory();
        }
    }
}

