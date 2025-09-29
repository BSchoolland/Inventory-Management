using System.Media;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Inventory_Management
{
    public partial class Form2 : Form
    {
        private Form1 form1;
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
            LoadInventory();
            ClearFilters();
            ApplyFilters();
            displayInventory();
        }

        private void LoadInventory()
        {
            string jsonData = @"[ { ""items"": [ { ""name"": ""Great Value Whole Milk"", ""description"": ""1 gallon jug, 2% reduced fat milk"", ""current_price"": 3.28, ""stock_quantity"": 120, ""barcode"": ""078742054355"" }, { ""name"": ""Wonder Classic White Bread"", ""description"": ""20 oz loaf of sliced white bread"", ""current_price"": 2.48, ""stock_quantity"": 80, ""barcode"": ""072250010002"" }, { ""name"": ""Lay's Classic Potato Chips"", ""description"": ""8 oz party size bag of potato chips"", ""current_price"": 4.29, ""stock_quantity"": 60, ""barcode"": ""028400044709"" }, { ""name"": ""Great Value Large Eggs"", ""description"": ""12 count carton of Grade A large eggs"", ""current_price"": 2.29, ""stock_quantity"": 150, ""barcode"": ""078742059732"" }, { ""name"": ""Coca-Cola 12 Pack"", ""description"": ""12 fl oz cans, 12 count"", ""current_price"": 6.98, ""stock_quantity"": 90, ""barcode"": ""049000050103"" }, { ""name"": ""Bounty Paper Towels"", ""description"": ""6 double rolls, white, select-a-size"", ""current_price"": 12.97, ""stock_quantity"": 45, ""barcode"": ""037000748194"" }, { ""name"": ""Charmin Ultra Soft Toilet Paper"", ""description"": ""12 mega rolls, 2-ply"", ""current_price"": 14.97, ""stock_quantity"": 50, ""barcode"": ""030772064092"" }, { ""name"": ""Hanes Men’s T-Shirt 5-Pack"", ""description"": ""Crew neck, size L, assorted colors"", ""current_price"": 17.84, ""stock_quantity"": 35, ""barcode"": ""038257587281"" }, { ""name"": ""George Men’s Jeans"", ""description"": ""Straight fit, dark wash, size 34x32"", ""current_price"": 19.97, ""stock_quantity"": 40, ""barcode"": ""887778123441"" }, { ""name"": ""Mainstays Bath Towel"", ""description"": ""100% cotton, 27 in x 52 in, navy blue"", ""current_price"": 4.88, ""stock_quantity"": 70, ""barcode"": ""787139693250"" }, { ""name"": ""Ozark Trail Stainless Steel Tumbler"", ""description"": ""30 oz insulated tumbler with lid, silver"", ""current_price"": 9.94, ""stock_quantity"": 55, ""barcode"": ""810055480239"" }, { ""name"": ""Samsung 32-inch Smart TV"", ""description"": ""HD 720p with built-in apps"", ""current_price"": 158.00, ""stock_quantity"": 12, ""barcode"": ""887276567321"" }, { ""name"": ""Duracell AA Batteries"", ""description"": ""16 pack of alkaline AA batteries"", ""current_price"": 14.24, ""stock_quantity"": 65, ""barcode"": ""041333150014"" }, { ""name"": ""Crayola Crayons 24 Count"", ""description"": ""Classic color crayons for kids"", ""current_price"": 1.47, ""stock_quantity"": 200, ""barcode"": ""071662000249"" }, { ""name"": ""LEGO Classic Medium Brick Box"", ""description"": ""484-piece building set, ages 4+"", ""current_price"": 34.76, ""stock_quantity"": 25, ""barcode"": ""673419232904"" } ] } ]";
            var doc = JsonDocument.Parse(jsonData);
            var items = doc.RootElement[0].GetProperty("items");
            foreach (var item in items.EnumerateArray())
            {
                inventoryItems.Add(new InventoryItem
                {
                    Name = item.GetProperty("name").GetString(),
                    Description = item.GetProperty("description").GetString(),
                    CurrentPrice = item.GetProperty("current_price").GetDecimal(),
                    StockQuantity = item.GetProperty("stock_quantity").GetInt32(),
                    Barcode = item.GetProperty("barcode").GetString()
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
