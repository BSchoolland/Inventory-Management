using System.Media;
using Inventory_Management.Services;
using Inventory_Management.Models;

namespace Inventory_Management.Forms
{
    public partial class InventoryViewForm : Form
    {
        private OverviewForm overviewForm;
        private ManageItemsForm manageItemsForm;
        private AddStockForm addStockForm;
        private InventoryManager inventoryManager;
        private List<InventoryItem> filteredItems = new();

        public InventoryViewForm(OverviewForm parentForm)
        {
            overviewForm = parentForm;
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
            manageItemsForm = new ManageItemsForm(overviewForm, this);
            addStockForm = new AddStockForm(overviewForm, this, manageItemsForm);
            
            inventoryManager = new InventoryManager();
            dataGridViewInventory.DataSource = inventoryManager.BindingSource;
            ConfigureDataGridViewColumns();

            var nav = new NavigationControl(NavigationControl.NavigationPage.ViewInventory);
            nav.Location = new Point(0, 0);
            Controls.Add(nav);

            nav.OverviewClicked += (s, e) => { overviewForm.Show(); this.Hide(); };
            nav.ViewInventoryClicked += (s, e) => { SystemSounds.Hand.Play(); };
            nav.ManageItemsClicked += (s, e) => { manageItemsForm.Show(); this.Hide(); };
            nav.AddStockClicked += (s, e) => { addStockForm.Show(); this.Hide(); };
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
            buttonBackward.Enabled = false;
            buttonForward.Enabled = false;
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

        private void Form2_Load(object sender, EventArgs e)
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
    }

}

