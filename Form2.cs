using System.Media;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Inventory_Management
{
    public partial class Form2 : Form
    {
        private Form1 form1;
        private Form3 form3;
        private List<InventoryItem> inventoryItems = new();
        private int currentPage = 0;
        private int itemsPerPage = 5;
        private int totalPages = 0;
        private List<InventoryItem> filteredItems = new();

        public Form2(Form1 parentForm)
        {
            form1 = parentForm;
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
            form3 = new Form3(form1, this);
            var nav = new NavigationControl();
            nav.Location = new Point(0, 0);
            Controls.Add(nav);
            if (groupBox1 != null) groupBox1.Visible = false;

            nav.OverviewClicked += (s, e) => { form1.Show(); this.Hide(); };
            nav.ViewInventoryClicked += (s, e) => { SystemSounds.Hand.Play(); };
            nav.ManageItemsClicked += (s, e) => { form3.Show(); this.Hide(); };
            nav.AddStockClicked += (s, e) => SystemSounds.Beep.Play();
            nav.ProjectionsClicked += (s, e) => SystemSounds.Beep.Play();
            nav.CheckoutClicked += (s, e) => SystemSounds.Beep.Play();
            LoadInventory();
            ClearFilters();
            ApplyFilters();
            displayInventory();
            this.Activated += (s, e) => RefreshFromStorage();
        }

        private void LoadInventory()
        {
            EnsureDataFile();
            string jsonData = File.ReadAllText(GetDataFilePath());
            var doc = JsonDocument.Parse(jsonData);
            var items = doc.RootElement[0].GetProperty("items");
            foreach (var item in items.EnumerateArray())
            {
                inventoryItems.Add(new InventoryItem
                {
                    Name = item.GetProperty("name").GetString(),
                    Description = item.TryGetProperty("description", out var d) ? d.GetString() : string.Empty,
                    CurrentPrice = item.GetProperty("current_price").GetDecimal(),
                    StockQuantity = item.GetProperty("stock_quantity").GetInt32(),
                    Barcode = item.TryGetProperty("barcode", out var b) ? b.GetString() : string.Empty
                });
            }
        }

        private void ApplyFilters()
        {
            string search = searchTextBox.Text.Trim().ToLower();
            bool priceMinBlank = string.IsNullOrWhiteSpace(priceMinUpDown.Text);
            bool priceMaxBlank = string.IsNullOrWhiteSpace(priceMaxUpDown.Text);
            bool stockMinBlank = string.IsNullOrWhiteSpace(stockMinUpDown.Text);
            bool stockMaxBlank = string.IsNullOrWhiteSpace(stockMaxUpDown.Text);
            decimal priceMin = priceMinBlank ? decimal.MinValue : priceMinUpDown.Value;
            decimal priceMax = priceMaxBlank ? decimal.MaxValue : priceMaxUpDown.Value;
            int stockMin = stockMinBlank ? int.MinValue : (int)stockMinUpDown.Value;
            int stockMax = stockMaxBlank ? int.MaxValue : (int)stockMaxUpDown.Value;
            filteredItems = inventoryItems
                .Where(item => (string.IsNullOrEmpty(search) || item.Name.ToLower().Contains(search))
                    && (priceMinBlank && priceMaxBlank || item.CurrentPrice >= priceMin && item.CurrentPrice <= priceMax)
                    && (stockMinBlank && stockMaxBlank || item.StockQuantity >= stockMin && item.StockQuantity <= stockMax))
                .ToList();
            totalPages = (int)Math.Ceiling(filteredItems.Count / (double)itemsPerPage);
            if (currentPage >= totalPages) currentPage = 0;
        }

        private void displayInventory()
        {
            panelInventory.Controls.Clear();
            int startIdx = currentPage * itemsPerPage;
            int endIdx = Math.Min(startIdx + itemsPerPage, filteredItems.Count);
            int y = 10;
            for (int i = startIdx; i < endIdx; i++)
            {
                var item = filteredItems[i];
                var cardPanel = new Panel
                {
                    Location = new Point(10, y),
                    Size = new Size(570, 80),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.WhiteSmoke,
                    Padding = new Padding(10)
                };
                var nameLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Text = item.Name,
                    Location = new Point(10, 10)
                };
                var descLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    Text = item.Description,
                    Location = new Point(10, 35)
                };
                var priceLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Text = $"Price: ${item.CurrentPrice}",
                    Location = new Point(400, 10)
                };
                var stockLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Text = $"Stock: {item.StockQuantity}",
                    Location = new Point(400, 35)
                };
                var barcodeLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    Text = $"Barcode: {item.Barcode}",
                    Location = new Point(10, 60)
                };
                cardPanel.Controls.Add(nameLabel);
                cardPanel.Controls.Add(descLabel);
                cardPanel.Controls.Add(priceLabel);
                cardPanel.Controls.Add(stockLabel);
                cardPanel.Controls.Add(barcodeLabel);
                panelInventory.Controls.Add(cardPanel);
                y += cardPanel.Height + 10;
            }
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

        private void Form2_Load(object sender, EventArgs e)
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
                inventoryItems.Clear();
                LoadInventory();
                currentPage = 0;
                ApplyFilters();
                displayInventory();
            }
            catch { }
        }

        private static string GetDataFilePath()
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "data");
            return Path.Combine(dir, "items.json");
        }

        private static void EnsureDataFile()
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, "items.json");
            if (!File.Exists(path))
            {
                string seed = "[ { \"items\": [] } ]";
                File.WriteAllText(path, seed);
            }
        }
    }

    public class InventoryItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public int StockQuantity { get; set; }
        public string Barcode { get; set; }
    }
}
