using System.Media;
using Inventory_Management.Services;
using Inventory_Management.Models;

namespace Inventory_Management.Forms
{
    public partial class ManageInventoryForm : Form
    {
        private OverviewForm overviewForm;
        private ManageItemsForm? manageItemsForm;
        private InventoryManager inventoryManager;
        private System.Windows.Forms.Timer searchDebounceTimer;
        private System.Windows.Forms.Timer filterDebounceTimer;

        public ManageInventoryForm(OverviewForm parentOverview)
        {
            overviewForm = parentOverview;
            
            // Initialize timers BEFORE InitializeComponent() to avoid null reference errors
            // Initialize search debounce timer
            searchDebounceTimer = new System.Windows.Forms.Timer();
            searchDebounceTimer.Interval = 750; // 0.75 seconds
            searchDebounceTimer.Tick += (s, e) =>
            {
                searchDebounceTimer.Stop();
                ApplyFilters();
            };

            // Initialize filter debounce timer
            filterDebounceTimer = new System.Windows.Forms.Timer();
            filterDebounceTimer.Interval = 750; // 0.75 seconds
            filterDebounceTimer.Tick += (s, e) =>
            {
                filterDebounceTimer.Stop();
                ApplyFilters();
            };

            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();

            inventoryManager = new InventoryManager();
            dataGridViewInventory.DataSource = inventoryManager.BindingSource;
            ConfigureDataGridViewColumns();

            var nav = new NavigationControl(NavigationControl.NavigationPage.ManageInventory);
            nav.Location = new Point(0, 0);
            Controls.Add(nav);

            nav.OverviewClicked += (s, e) => { overviewForm.Show(); this.Hide(); };
            nav.ManageInventoryClicked += (s, e) => { SystemSounds.Hand.Play(); };
            nav.ManageItemsClicked += (s, e) => { EnsureManageItemsForm().Show(); this.Hide(); };

            // Initialize filters after inventoryManager is ready
            ClearFilters();
            this.Activated += (s, e) => RefreshFromStorage();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            dataGridViewInventory.BackgroundColor = Color.FromArgb(50, 50, 50);
            dataGridViewInventory.GridColor = Color.FromArgb(70, 70, 70);
            dataGridViewInventory.EnableHeadersVisualStyles = false;

            var headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(40, 40, 40);
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle = headerStyle;

            var defaultStyle = new DataGridViewCellStyle();
            defaultStyle.BackColor = Color.FromArgb(50, 50, 50);
            defaultStyle.ForeColor = Color.White;
            defaultStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            defaultStyle.SelectionForeColor = Color.White;
            dataGridViewInventory.DefaultCellStyle = defaultStyle;
            dataGridViewInventory.RowHeadersDefaultCellStyle = defaultStyle;
        }

        /// <summary>
        /// Lazily initializes and returns the ManageItemsForm.
        /// </summary>
        private ManageItemsForm EnsureManageItemsForm()
        {
            if (manageItemsForm == null)
            {
                manageItemsForm = new ManageItemsForm(overviewForm, this);
            }
            return manageItemsForm;
        }

        /// <summary>
        /// Configures DataGridView columns with proper formatting for currency and numbers.
        /// </summary>
        private void ConfigureDataGridViewColumns()
        {
            // Clear any default columns
            dataGridViewInventory.Columns.Clear();
            
            // Disable auto-generation of columns - only show explicitly defined columns
            dataGridViewInventory.AutoGenerateColumns = false;

            var baseStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(0, 122, 204),
                SelectionForeColor = Color.White
            };

            // Name column - limited width with text wrapping
            var nameStyle = new DataGridViewCellStyle(baseStyle) { WrapMode = DataGridViewTriState.True };
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Item Name",
                Width = 200,
                DefaultCellStyle = nameStyle
            });

            // Description column - limited width with text wrapping
            var descStyle = new DataGridViewCellStyle(baseStyle) { WrapMode = DataGridViewTriState.True };
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                DataPropertyName = "Description",
                HeaderText = "Description",
                Width = 200,
                DefaultCellStyle = descStyle
            });

            // Price column - formatted as currency
            var priceStyle = new DataGridViewCellStyle(baseStyle) { Format = "c2", Alignment = DataGridViewContentAlignment.MiddleLeft };
            var priceColumn = new DataGridViewTextBoxColumn
            {
                Name = "Price",
                DataPropertyName = "CurrentPrice",
                HeaderText = "Price",
                Width = 100,
                DefaultCellStyle = priceStyle
            };
            dataGridViewInventory.Columns.Add(priceColumn);

            // Stock Quantity column - left-aligned with commas
            var qtyStyle = new DataGridViewCellStyle(baseStyle) { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleLeft };
            var quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "StockQuantity",
                DataPropertyName = "StockQuantity",
                HeaderText = "Stock",
                Width = 80,
                DefaultCellStyle = qtyStyle
            };
            dataGridViewInventory.Columns.Add(quantityColumn);

            // Enable auto-sizing for rows to accommodate wrapped text
            dataGridViewInventory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewInventory.RowTemplate.Height = 60;
        }

        private void ApplyFilters()
        {
            if (inventoryManager == null) return;

            string search = searchTextBox.Text.Trim();
            bool priceMinBlank = string.IsNullOrWhiteSpace(priceMinUpDown.Text);
            bool priceMaxBlank = string.IsNullOrWhiteSpace(priceMaxUpDown.Text);
            bool stockMinBlank = string.IsNullOrWhiteSpace(stockMinUpDown.Text);
            bool stockMaxBlank = string.IsNullOrWhiteSpace(stockMaxUpDown.Text);
            decimal? priceMin = priceMinBlank ? null : priceMinUpDown.Value;
            decimal? priceMax = priceMaxBlank ? null : priceMaxUpDown.Value;
            int? stockMin = stockMinBlank ? null : (int)stockMinUpDown.Value;
            int? stockMax = stockMaxBlank ? null : (int)stockMaxUpDown.Value;

            bool hasSearch = !string.IsNullOrWhiteSpace(search);
            bool hasFilters = priceMin.HasValue || priceMax.HasValue || stockMin.HasValue || stockMax.HasValue;

            if (hasSearch && hasFilters)
            {
                // Both search and filters - set filters first, then search (which will combine them)
                inventoryManager.FilterItems(priceMin, priceMax, stockMin, stockMax);
                inventoryManager.SearchItemsWithActiveFilters(search);
            }
            else if (hasSearch)
            {
                inventoryManager.SearchItems(search);
            }
            else if (hasFilters)
            {
                inventoryManager.FilterItems(priceMin, priceMax, stockMin, stockMax);
            }
            else
            {
                inventoryManager.LoadInventoryPage(1);
            }

            UpdatePageInfo();
        }

        /// <summary>
        /// Initializes the inventory data. Can be called explicitly to pre-load data.
        /// </summary>
        public void InitializeInventory()
        {
            if (inventoryManager != null)
            {
                inventoryManager.Initialize();
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            // Reset the debounce timer on each keystroke
            searchDebounceTimer.Stop();
            searchDebounceTimer.Start();
        }

        private void filter_ValueChanged(object sender, EventArgs e)
        {
            // Reset the debounce timer on each value change
            filterDebounceTimer.Stop();
            filterDebounceTimer.Start();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (inventoryManager.NextPage())
            {
                UpdatePageInfo();
            }
        }

        private void buttonBackward_Click(object sender, EventArgs e)
        {
            if (inventoryManager.PreviousPage())
            {
                UpdatePageInfo();
            }
        }

        private void UpdatePageInfo()
        {
            buttonBackward.Enabled = inventoryManager.CurrentPage > 1;
            buttonForward.Enabled = inventoryManager.CurrentPage < inventoryManager.TotalPages;

            itemCountLabel.Text = $"Items: {inventoryManager.TotalItems}";
            pageInfoLabel.Text = $"Page {inventoryManager.CurrentPage} of {inventoryManager.TotalPages}";
        }

        private void clearFiltersButton_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void ClearFilters()
        {
            searchTextBox.Text = "";
            priceMinUpDown.Text = "";
            priceMaxUpDown.Text = "";
            stockMinUpDown.Text = "";
            stockMaxUpDown.Text = "";
            if (inventoryManager != null)
            {
                inventoryManager.LoadInventoryPage(1);
                UpdatePageInfo();
            }
        }

        private void ManageInventoryForm_Load(object sender, EventArgs e)
        {
            // Initialize inventory manager after form is shown to avoid blocking UI on startup
            inventoryManager.Initialize();
        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            // Reset the debounce timer on each text change
            filterDebounceTimer.Stop();
            filterDebounceTimer.Start();
        }

        private void RefreshFromStorage()
        {
            try
            {
                inventoryManager.Refresh();
                UpdatePageInfo();
            }
            catch { }
        }

        /// <summary>
        /// Handles adding a new item to the inventory.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            // Validate and parse input
            if (!decimal.TryParse(priceTextBox.Text, out var price))
            {
                MessageBox.Show("Invalid price format. Please enter a valid decimal number.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                priceTextBox.Focus();
                return;
            }

            if (!int.TryParse(qtyTextBox.Text, out var qty))
            {
                MessageBox.Show("Invalid quantity format. Please enter a valid integer.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qtyTextBox.Focus();
                return;
            }

            // Attempt to add the item
            if (inventoryManager.TryAddItem(nameTextBox.Text, descriptionTextBox.Text, price, qty, string.Empty, out var errorMsg))
            {
                MessageBox.Show($"Item '{nameTextBox.Text}' added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Clear form
                nameTextBox.Clear();
                descriptionTextBox.Clear();
                priceTextBox.Clear();
                qtyTextBox.Clear();
                RefreshFromStorage();
            }
            else
            {
                MessageBox.Show($"Failed to add item: {errorMsg}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Increments the stock quantity of the selected item.
        /// </summary>
        private void incButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to increment.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(incQtyTextBox.Text, out var amount))
            {
                MessageBox.Show("Invalid amount format. Please enter a valid integer.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                incQtyTextBox.Focus();
                return;
            }

            int rowIndex = dataGridViewInventory.SelectedRows[0].Index;
            if (inventoryManager.TryIncrementStock(rowIndex, amount, out var errorMsg))
            {
                RefreshFromStorage();
            }
            else
            {
                MessageBox.Show($"Failed to increment stock: {errorMsg}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Decrements the stock quantity of the selected item.
        /// </summary>
        private void decButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to decrement.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(incQtyTextBox.Text, out var amount))
            {
                MessageBox.Show("Invalid amount format. Please enter a valid integer.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                incQtyTextBox.Focus();
                return;
            }

            int rowIndex = dataGridViewInventory.SelectedRows[0].Index;
            if (inventoryManager.TryIncrementStock(rowIndex, -amount, out var errorMsg))
            {
                RefreshFromStorage();
            }
            else
            {
                MessageBox.Show($"Failed to decrement stock: {errorMsg}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Deletes the selected item from inventory with confirmation.
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = dataGridViewInventory.SelectedRows[0].Index;
            var selectedItem = (dynamic)dataGridViewInventory.Rows[rowIndex].DataBoundItem;
            string itemName = selectedItem?.Name ?? "Unknown";

            // Ask for confirmation
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete '{itemName}'? This action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (inventoryManager.TryDeleteItem(rowIndex, out var errorMsg))
                {
                    MessageBox.Show($"Item '{itemName}' deleted successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshFromStorage();
                }
                else
                {
                    MessageBox.Show($"Failed to delete item: {errorMsg}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

