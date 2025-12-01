using System.Media;
using Inventory_Management.Services;
using Inventory_Management.Models;

namespace Inventory_Management.Forms
{
    public partial class AddStockForm : Form
    {
        private OverviewForm overviewForm;
        private InventoryViewForm inventoryViewForm;
        private ManageItemsForm manageItemsForm;
        private InventoryManager inventoryManager;
        private List<InventoryItem> filteredItems = new();

        public AddStockForm(OverviewForm parentOverview, InventoryViewForm parentInventoryView, ManageItemsForm parentManageItems)
        {
            overviewForm = parentOverview;
            inventoryViewForm = parentInventoryView;
            manageItemsForm = parentManageItems;
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();

            inventoryManager = new InventoryManager();
            dataGridViewInventory.DataSource = inventoryManager.BindingSource;
            ConfigureDataGridViewColumns();

            var nav = new NavigationControl(NavigationControl.NavigationPage.AddStock);
            nav.Location = new Point(0, 0);
            Controls.Add(nav);

            nav.OverviewClicked += (s, e) => { overviewForm.Show(); this.Hide(); };
            nav.ViewInventoryClicked += (s, e) => { inventoryViewForm.Show(); this.Hide(); };
            nav.ManageItemsClicked += (s, e) => { manageItemsForm.Show(); this.Hide(); };
            nav.AddStockClicked += (s, e) => { SystemSounds.Hand.Play(); };
            nav.ProjectionsClicked += (s, e) => SystemSounds.Beep.Play();
            nav.CheckoutClicked += (s, e) => SystemSounds.Beep.Play();

            // Initialize filters after inventoryManager is ready
            ClearFilters();
            this.Activated += (s, e) => RefreshFromStorage();
        }

        /// <summary>
        /// Configures DataGridView columns with proper formatting for currency and numbers.
        /// </summary>
        private void ConfigureDataGridViewColumns()
        {
            // Clear any default columns
            dataGridViewInventory.Columns.Clear();

            // Name column
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Item Name",
                Width = 150
            });

            // Description column
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                DataPropertyName = "Description",
                HeaderText = "Description",
                Width = 120
            });

            // Price column - formatted as currency
            var priceColumn = new DataGridViewTextBoxColumn
            {
                Name = "Price",
                DataPropertyName = "CurrentPrice",
                HeaderText = "Price",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "c2",
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };
            dataGridViewInventory.Columns.Add(priceColumn);

            // Stock Quantity column - left-aligned with commas
            var quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "StockQuantity",
                DataPropertyName = "StockQuantity",
                HeaderText = "Stock",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };
            dataGridViewInventory.Columns.Add(quantityColumn);

            // Barcode column
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Barcode",
                DataPropertyName = "Barcode",
                HeaderText = "Barcode",
                Width = 120
            });
        }

        private void ApplyFilters()
        {
            if (inventoryManager == null) return;

            string search = searchTextBox.Text;
            bool priceMinBlank = string.IsNullOrWhiteSpace(priceMinUpDown.Text);
            bool priceMaxBlank = string.IsNullOrWhiteSpace(priceMaxUpDown.Text);
            bool stockMinBlank = string.IsNullOrWhiteSpace(stockMinUpDown.Text);
            bool stockMaxBlank = string.IsNullOrWhiteSpace(stockMaxUpDown.Text);
            decimal? priceMin = priceMinBlank ? null : priceMinUpDown.Value;
            decimal? priceMax = priceMaxBlank ? null : priceMaxUpDown.Value;
            int? stockMin = stockMinBlank ? null : (int)stockMinUpDown.Value;
            int? stockMax = stockMaxBlank ? null : (int)stockMaxUpDown.Value;
            filteredItems = InventoryFilters.Apply(inventoryManager.MasterInventory, search, priceMin, priceMax, stockMin, stockMax);
            DisplayFilteredInventory();
        }

        private void DisplayFilteredInventory()
        {
            // Update DataGridView with filtered items
            inventoryManager.BindingSource.DataSource = new BindingSource(filteredItems, null);
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void filter_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            // Pagination removed - DataGridView displays all filtered items
        }

        private void buttonBackward_Click(object sender, EventArgs e)
        {
            // Pagination removed - DataGridView displays all filtered items
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
                ApplyFilters();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e)
        {
            manageItemsForm.Show();
            this.Hide();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            overviewForm.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void RefreshFromStorage()
        {
            try
            {
                inventoryManager.Refresh();
                ApplyFilters();
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
            if (inventoryManager.TryAddItem(nameTextBox.Text, descriptionTextBox.Text, price, qty, barcodeTextBox.Text, out var errorMsg))
            {
                MessageBox.Show($"Item '{nameTextBox.Text}' added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Clear form
                nameTextBox.Clear();
                descriptionTextBox.Clear();
                priceTextBox.Clear();
                qtyTextBox.Clear();
                barcodeTextBox.Clear();
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

    }

}

