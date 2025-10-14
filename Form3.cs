using System.Media;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory_Management
{
    public partial class Form3 : Form
    {
        private Form1 form1;
        private Form2 form2;

        public Form3(Form1 parent1, Form2 parent2)
        {
            form1 = parent1;
            form2 = parent2;
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            // Overview -> go to Form1
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // View Inventory -> go to Form2
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e) { }

        private void button4_Click(object sender, EventArgs e)
        {
            // Create Item -> already here
            SystemSounds.Hand.Play();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                using var ofd = new OpenFileDialog();
                ofd.Title = "Select items file";
                ofd.Filter = "Text/CSV Files|*.txt;*.csv|All Files|*.*";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                var lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                if (lines.Length == 0)
                {
                    MessageBox.Show("Selected file is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var dataPath = GetDataFilePath();
                EnsureDataFile();

                var rootList = LoadStorage();
                if (rootList.Count == 0)
                {
                    rootList.Add(new StorageRoot { Items = new List<StorageItem>() });
                }
                var storage = rootList[0];

                int added = 0, updated = 0, skipped = 0, errors = 0;
                foreach (var raw in lines)
                {
                    if (string.IsNullOrWhiteSpace(raw)) { continue; }
                    var parts = raw.Split(',');
                    if (parts.Length != 3)
                    {
                        errors++;
                        continue;
                    }
                    string name = parts[0].Trim();
                    if (string.IsNullOrWhiteSpace(name)) { errors++; continue; }
                    if (!decimal.TryParse(parts[1].Trim(), out var price)) { errors++; continue; }
                    if (!int.TryParse(parts[2].Trim(), out var qty)) { errors++; continue; }

                    var existing = storage.Items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
                    if (existing != null)
                    {
                        var resp = MessageBox.Show($"'{name}' exists. Update price/quantity?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resp == DialogResult.Yes)
                        {
                            existing.CurrentPrice = price;
                            existing.StockQuantity = qty;
                            updated++;
                        }
                        else
                        {
                            skipped++;
                        }
                    }
                    else
                    {
                        storage.Items.Add(new StorageItem
                        {
                            Name = name,
                            Description = string.Empty,
                            CurrentPrice = price,
                            StockQuantity = qty,
                            Barcode = string.Empty
                        });
                        added++;
                    }
                }

                SaveStorage(rootList);
                MessageBox.Show($"Added: {added}\nUpdated: {updated}\nSkipped: {skipped}\nErrors: {errors}", "Upload Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to upload items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                File.WriteAllText(path, "[ { \"items\": [] } ]");
            }
        }

        private static List<StorageRoot> LoadStorage()
        {
            string path = GetDataFilePath();
            var json = File.ReadAllText(path);
            var list = JsonSerializer.Deserialize<List<StorageRoot>>(json) ?? new List<StorageRoot>();
            return list;
        }

        private static void SaveStorage(List<StorageRoot> roots)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(roots, options);
            File.WriteAllText(GetDataFilePath(), json);
        }

        private class StorageRoot
        {
            [JsonPropertyName("items")] public List<StorageItem> Items { get; set; } = new();
        }

        private class StorageItem
        {
            [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
            [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
            [JsonPropertyName("current_price")] public decimal CurrentPrice { get; set; }
            [JsonPropertyName("stock_quantity")] public int StockQuantity { get; set; }
            [JsonPropertyName("barcode")] public string Barcode { get; set; } = string.Empty;
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}


