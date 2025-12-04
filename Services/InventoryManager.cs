using System.ComponentModel;
using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    /// <summary>
    /// Centralizes inventory management operations with binding support for UI controls.
    /// Supports pagination to efficiently handle large datasets.
    /// </summary>
    public class InventoryManager : INotifyPropertyChanged
    {
        private List<InventoryItem> _currentPageInventory = new();
        private BindingSource _bindingSource = new();
        private int _currentPage = 1;
        private int _pageSize = 100;
        private int _totalItems = 0;
        private string _currentSearchTerm = string.Empty;
        private decimal? _currentFilterMinPrice;
        private decimal? _currentFilterMaxPrice;
        private int? _currentFilterMinStock;
        private int? _currentFilterMaxStock;
        private bool _isInitialized = false;

        public event PropertyChangedEventHandler? PropertyChanged { add { } remove { } }

        public InventoryManager()
        {
            _bindingSource.DataSource = _currentPageInventory;
        }

        /// <summary>
        /// Initializes the inventory manager with data from the database.
        /// Call this after the form is shown to avoid blocking the UI on startup.
        /// </summary>
        public void Initialize()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                LoadInventoryPage(1);
            }
        }

        /// <summary>
        /// Gets the BindingSource for DataGridView binding.
        /// </summary>
        public BindingSource BindingSource => _bindingSource;

        /// <summary>
        /// Gets the current page inventory list.
        /// </summary>
        public List<InventoryItem> CurrentPageInventory => _currentPageInventory;

        /// <summary>
        /// Gets the current page number (1-based).
        /// </summary>
        public int CurrentPage => _currentPage;

        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int PageSize => _pageSize;

        /// <summary>
        /// Gets the total number of items in the database.
        /// </summary>
        public int TotalItems => _totalItems;

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages => _totalItems == 0 ? 1 : (int)Math.Ceiling((double)_totalItems / _pageSize);

        /// <summary>
        /// Loads inventory for the specified page.
        /// </summary>
        public void LoadInventoryPage(int pageNumber)
        {
            try
            {
                _currentPage = pageNumber;
                _currentSearchTerm = string.Empty;
                _totalItems = InventoryStorageSqlite.GetTotalItemCount();
                _currentPageInventory = InventoryStorageSqlite.LoadItemsPaged(_currentPage, _pageSize);
                RefreshBindingSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load inventory: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Searches items by name and loads the first page of results.
        /// </summary>
        public void SearchItems(string searchTerm)
        {
            try
            {
                _currentSearchTerm = searchTerm;
                _currentPage = 1;
                _currentPageInventory = InventoryStorageSqlite.SearchItems(searchTerm, _currentPage, _pageSize, out _totalItems);
                RefreshBindingSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search failed: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filters items by criteria and loads the first page of results.
        /// </summary>
        public void FilterItems(decimal? minPrice, decimal? maxPrice, int? minStock, int? maxStock)
        {
            try
            {
                // Store filter values for page navigation
                _currentFilterMinPrice = minPrice;
                _currentFilterMaxPrice = maxPrice;
                _currentFilterMinStock = minStock;
                _currentFilterMaxStock = maxStock;
                
                // If there's an active search, combine search with filters
                if (!string.IsNullOrWhiteSpace(_currentSearchTerm))
                {
                    _currentPage = 1;
                    _currentPageInventory = InventoryStorageSqlite.SearchAndFilterItems(
                        _currentSearchTerm, minPrice, maxPrice, minStock, maxStock, 
                        _currentPage, _pageSize, out _totalItems);
                }
                else
                {
                    _currentSearchTerm = string.Empty;
                    _currentPage = 1;
                    _currentPageInventory = InventoryStorageSqlite.FilterItems(minPrice, maxPrice, minStock, maxStock, _currentPage, _pageSize, out _totalItems);
                }
                
                RefreshBindingSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filter failed: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Searches items by name. If filters are active, combines them with AND logic.
        /// </summary>
        public void SearchItemsWithActiveFilters(string searchTerm)
        {
            try
            {
                _currentSearchTerm = searchTerm;
                _currentPage = 1;
                
                // If there are active filters, combine search with filters
                if (_currentFilterMinPrice.HasValue || _currentFilterMaxPrice.HasValue || 
                    _currentFilterMinStock.HasValue || _currentFilterMaxStock.HasValue)
                {
                    _currentPageInventory = InventoryStorageSqlite.SearchAndFilterItems(
                        searchTerm, _currentFilterMinPrice, _currentFilterMaxPrice, 
                        _currentFilterMinStock, _currentFilterMaxStock, 
                        _currentPage, _pageSize, out _totalItems);
                }
                else
                {
                    _currentPageInventory = InventoryStorageSqlite.SearchItems(searchTerm, _currentPage, _pageSize, out _totalItems);
                }
                
                RefreshBindingSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search failed: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads the next page of results.
        /// </summary>
        public bool NextPage()
        {
            if (_currentPage >= TotalPages)
                return false;

            LoadPageInternal(_currentPage + 1);
            return true;
        }

        /// <summary>
        /// Loads the previous page of results.
        /// </summary>
        public bool PreviousPage()
        {
            if (_currentPage <= 1)
                return false;

            LoadPageInternal(_currentPage - 1);
            return true;
        }

        /// <summary>
        /// Loads a specific page of results.
        /// </summary>
        public bool GoToPage(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > TotalPages)
                return false;

            LoadPageInternal(pageNumber);
            return true;
        }

        private void LoadPageInternal(int pageNumber)
        {
            try
            {
                _currentPage = pageNumber;
                
                // Check if there are active filters
                bool hasFilters = _currentFilterMinPrice.HasValue || _currentFilterMaxPrice.HasValue || 
                                 _currentFilterMinStock.HasValue || _currentFilterMaxStock.HasValue;
                
                if (string.IsNullOrWhiteSpace(_currentSearchTerm) && !hasFilters)
                {
                    // No search and no filters - load all items for this page
                    _currentPageInventory = InventoryStorageSqlite.LoadItemsPaged(_currentPage, _pageSize);
                }
                else if (!string.IsNullOrWhiteSpace(_currentSearchTerm) && hasFilters)
                {
                    // Both search and filters active - combine them
                    _currentPageInventory = InventoryStorageSqlite.SearchAndFilterItems(
                        _currentSearchTerm, _currentFilterMinPrice, _currentFilterMaxPrice, 
                        _currentFilterMinStock, _currentFilterMaxStock, 
                        _currentPage, _pageSize, out _totalItems);
                }
                else if (!string.IsNullOrWhiteSpace(_currentSearchTerm))
                {
                    // Only search active
                    _currentPageInventory = InventoryStorageSqlite.SearchItems(_currentSearchTerm, _currentPage, _pageSize, out _totalItems);
                }
                else
                {
                    // Only filters active
                    _currentPageInventory = InventoryStorageSqlite.FilterItems(
                        _currentFilterMinPrice, _currentFilterMaxPrice, 
                        _currentFilterMinStock, _currentFilterMaxStock, 
                        _currentPage, _pageSize, out _totalItems);
                }
                
                RefreshBindingSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load page: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gets item name suggestions for autocomplete.
        /// </summary>
        public List<string> GetItemNameSuggestions(string searchTerm, int limit = 5)
        {
            try
            {
                return InventoryStorageSqlite.GetItemNamesBySearchTerm(searchTerm, limit);
            }
            catch
            {
                return new List<string>();
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

            var item = new InventoryItem
            {
                Name = name.Trim(),
                Description = description?.Trim() ?? string.Empty,
                CurrentPrice = price,
                StockQuantity = quantity,
                Barcode = barcode?.Trim() ?? string.Empty
            };

            try
            {
                InventoryStorageSqlite.AddItem(item);
                _totalItems++;
                // Reload current page to show the new item if it belongs to current view
                LoadPageInternal(_currentPage);
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = $"Failed to add item: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Updates an item in the database.
        /// </summary>
        public bool TryUpdateItem(InventoryItem item, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                InventoryStorageSqlite.UpdateItem(item);
                // Update in current page view
                var existingItem = _currentPageInventory.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Name = item.Name;
                    existingItem.Description = item.Description;
                    existingItem.CurrentPrice = item.CurrentPrice;
                    existingItem.StockQuantity = item.StockQuantity;
                    existingItem.Barcode = item.Barcode;
                    _bindingSource.ResetBindings(false);
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = $"Failed to update item: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Increments stock quantity for an item on the current page.
        /// </summary>
        public bool TryIncrementStock(int rowIndex, int amount, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (rowIndex < 0 || rowIndex >= _currentPageInventory.Count)
            {
                errorMsg = "Invalid row selection.";
                return false;
            }

            var item = _currentPageInventory[rowIndex];
            if (item.StockQuantity + amount < 0)
            {
                errorMsg = $"Cannot decrease stock. '{item.Name}' would drop below 0.";
                return false;
            }

            item.StockQuantity += amount;
            
            try
            {
                InventoryStorageSqlite.UpdateItem(item);
                _bindingSource.ResetBindings(false);
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = $"Failed to update stock: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        public bool TryDeleteItem(int rowIndex, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (rowIndex < 0 || rowIndex >= _currentPageInventory.Count)
            {
                errorMsg = "Invalid row selection.";
                return false;
            }

            var item = _currentPageInventory[rowIndex];

            try
            {
                InventoryStorageSqlite.DeleteItem(item.Id);
                _totalItems--;
                // Reload current page after deletion
                LoadPageInternal(_currentPage);
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = $"Failed to delete item: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Refreshes the current page from the database.
        /// </summary>
        public void Refresh()
        {
            LoadPageInternal(_currentPage);
        }

        private void RefreshBindingSource()
        {
            _bindingSource.DataSource = new BindingSource(_currentPageInventory, null);
        }
    }
}
