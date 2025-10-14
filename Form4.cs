using System.Media;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Inventory_Management
{
    public partial class Form4 : Form
    {
        private Form1 form1;
        private Form2 form2;
        private Form3 form3;
        private List<InventoryItem> inventoryItems = new();
        private int currentPage = 0;
        private int itemsPerPage = 5;
        private int totalPages = 0;
        private List<InventoryItem> filteredItems = new();

        public Form4(Form1 parent1, Form2 parent2, Form3 parent3)
        {
            form1 = parent1;
            form2 = parent2;
            form3 = parent3;
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
            var nav = new NavigationControl(NavigationControl.NavigationPage.AddStock);
            nav.Location = new Point(0, 0);
            Controls.Add(nav);
            

            nav.OverviewClicked += (s, e) => { form1.Show(); this.Hide(); };
            nav.ViewInventoryClicked += (s, e) => { form2.Show(); this.Hide(); };
            nav.ManageItemsClicked += (s, e) => { form3.Show(); this.Hide(); };
            nav.AddStockClicked += (s, e) => { SystemSounds.Hand.Play(); };
            nav.ProjectionsClicked += (s, e) => SystemSounds.Beep.Play();
            nav.CheckoutClicked += (s, e) => SystemSounds.Beep.Play();
            inventoryItems = InventoryStorage.LoadItems();
            ClearFilters();
            ApplyFilters();
            displayInventory();
            this.Activated += (s, e) => RefreshFromStorage();
        }

        private void LoadInventory() { inventoryItems = InventoryStorage.LoadItems(); }

        private void ApplyFilters()
        {
            string search = searchTextBox.Text;
            bool priceMinBlank = string.IsNullOrWhiteSpace(priceMinUpDown.Text);
            bool priceMaxBlank = string.IsNullOrWhiteSpace(priceMaxUpDown.Text);
            bool stockMinBlank = string.IsNullOrWhiteSpace(stockMinUpDown.Text);
            bool stockMaxBlank = string.IsNullOrWhiteSpace(stockMaxUpDown.Text);
            decimal? priceMin = priceMinBlank ? null : priceMinUpDown.Value;
            decimal? priceMax = priceMaxBlank ? null : priceMaxUpDown.Value;
            int? stockMin = stockMinBlank ? null : (int)stockMinUpDown.Value;
            int? stockMax = stockMaxBlank ? null : (int)stockMaxUpDown.Value;
            filteredItems = InventoryFilters.Apply(inventoryItems, search, priceMin, priceMax, stockMin, stockMax);
            totalPages = (int)Math.Ceiling(filteredItems.Count / (double)itemsPerPage);
            if (currentPage >= totalPages) currentPage = 0;
        }

        private void displayInventory()
        {
            int startIdx = currentPage * itemsPerPage;
            int endIdx = Math.Min(startIdx + itemsPerPage, filteredItems.Count);
            InventoryUI.RenderCards(
                panelInventory,
                filteredItems,
                startIdx,
                endIdx,
                it => it.Name,
                it => it.Description,
                it => it.CurrentPrice,
                it => it.StockQuantity,
                it => it.Barcode,
                (it, addQty) =>
                {
                    if (addQty <= 0) return;
                    TryAddStock(it.Name, addQty);
                    RefreshFromStorage();
                });
            buttonBackward.Enabled = currentPage > 0;
            buttonForward.Enabled = currentPage < totalPages - 1;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            currentPage = 0;
            ApplyFilters();
            displayInventory();
        }

        private void filter_ValueChanged(object sender, EventArgs e)
        {
            currentPage = 0;
            ApplyFilters();
            displayInventory();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages - 1)
            {
                currentPage++;
                displayInventory();
            }
        }

        private void buttonBackward_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                displayInventory();
            }
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
            currentPage = 0;
            ApplyFilters();
            displayInventory();
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e)
        {
            form3.Show();
            this.Hide();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            form1.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            currentPage = 0;
            ApplyFilters();
            displayInventory();
        }

        private void RefreshFromStorage()
        {
            try
            {
                inventoryItems = InventoryStorage.LoadItems();
                currentPage = 0;
                ApplyFilters();
                displayInventory();
            }
            catch { }
        }
        private void TryAddStock(string name, int add)
        {
            try
            {
                var items = InventoryStorage.LoadItems();
                var target = items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
                if (target == null) return;
                target.StockQuantity += add;
                InventoryStorage.SaveItems(items);
            }
            catch { }
        }
    }

}
