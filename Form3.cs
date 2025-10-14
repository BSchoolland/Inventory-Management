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
        private NavigationControl nav;
		private List<string> cachedItemNames = new();

        public Form3(Form1 parent1, Form2 parent2)
        {
            form1 = parent1;
            form2 = parent2;
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();

            nav = new NavigationControl();
            nav.Location = new Point(0, 0);
            Controls.Add(nav);


            nav.OverviewClicked += (s, e) => button1_Click(s, e);
            nav.ViewInventoryClicked += (s, e) => button2_Click(s, e);
            nav.ManageItemsClicked += (s, e) => button4_Click(s, e);
            nav.AddStockClicked += (s, e) => SystemSounds.Beep.Play();
            nav.ProjectionsClicked += (s, e) => SystemSounds.Beep.Play();
            nav.CheckoutClicked += (s, e) => SystemSounds.Beep.Play();
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
                var bulkChoice = UpdateDecision.AskEach;
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
                        UpdateDecision decision = bulkChoice;
                        if (decision == UpdateDecision.AskEach)
                        {
                            decision = ShowBulkUpdateDialog(name);
                            if (decision == UpdateDecision.YesToAll || decision == UpdateDecision.SkipAll)
                            {
                                bulkChoice = decision;
                            }
                        }

                        if (decision == UpdateDecision.Yes || decision == UpdateDecision.YesToAll)
                        {
                            existing.CurrentPrice = price;
                            existing.StockQuantity = qty;
                            updated++;
                        }
                        else if (decision == UpdateDecision.Skip || decision == UpdateDecision.SkipAll)
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
				RefreshItemNames();
				UpdateDeleteUIState();
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var roots = LoadStorage();
                if (roots.Count == 0)
                {
                    MessageBox.Show("No storage found.");
                    return;
                }
                var storage = roots[0];
                var name = deleteNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Enter a name to delete.");
                    return;
                }

                var removed = storage.Items.RemoveAll(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
                if (removed > 0)
                {
                    SaveStorage(roots);
                    MessageBox.Show($"Deleted {removed} item(s) named '{name}'.");
					RefreshItemNames();
					UpdateDeleteUIState();
                }
                else
                {
                    MessageBox.Show($"No item found with name '{name}'.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete failed: {ex.Message}");
            }
        }

        private void addManualButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureDataFile();
                var roots = LoadStorage();
                if (roots.Count == 0)
                {
                    roots.Add(new StorageRoot { Items = new List<StorageItem>() });
                }
                var storage = roots[0];

                string name = nameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Name is required."); return; }
                if (!decimal.TryParse(priceTextBox.Text.Trim(), out var price)) { MessageBox.Show("Invalid price."); return; }
                if (!int.TryParse(qtyTextBox.Text.Trim(), out var qty)) { MessageBox.Show("Invalid quantity."); return; }

                var existing = storage.Items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    var resp = MessageBox.Show($"'{name}' exists. Update price/quantity?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resp == DialogResult.Yes)
                    {
                        existing.CurrentPrice = price;
                        existing.StockQuantity = qty;
                    }
                    else
                    {
                        MessageBox.Show("Add cancelled.");
                        return;
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
                }

				SaveStorage(roots);
				MessageBox.Show("Item saved.");
				RefreshItemNames();
				UpdateDeleteUIState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Add failed: {ex.Message}");
            }
        }

        private void deleteNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (cachedItemNames.Count == 0) RefreshItemNames();
            UpdateDeleteUIState();
        }

        private void deleteSuggestionsListBox_Click(object sender, EventArgs e)
        {
            if (deleteSuggestionsListBox.SelectedItem is string name)
            {
                deleteNameTextBox.Text = name;
                deleteSuggestionsListBox.Visible = false;
            }
        }

        private void UpdateDeleteUIState()
        {
            string text = deleteNameTextBox.Text.Trim();
            var matches = string.IsNullOrEmpty(text)
                ? new List<string>()
                : cachedItemNames
                    .Where(n => n.Contains(text, StringComparison.OrdinalIgnoreCase))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Take(5)
                    .ToList();

            deleteSuggestionsListBox.BeginUpdate();
            deleteSuggestionsListBox.Items.Clear();
            foreach (var m in matches) deleteSuggestionsListBox.Items.Add(m);
            deleteSuggestionsListBox.EndUpdate();
            deleteSuggestionsListBox.Visible = matches.Count > 0 && !matches.Any(m => string.Equals(m, text, StringComparison.OrdinalIgnoreCase));

            bool exact = cachedItemNames.Any(n => string.Equals(n, text, StringComparison.OrdinalIgnoreCase));
            deleteButton.Enabled = exact;
        }

        private void RefreshItemNames()
        {
            try
            {
                var roots = LoadStorage();
                var list = roots.Count > 0 ? roots[0].Items.Select(i => i.Name).Where(n => !string.IsNullOrWhiteSpace(n)).ToList() : new List<string>();
                cachedItemNames = list;
            }
            catch
            {
                cachedItemNames = new List<string>();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            RefreshItemNames();
            UpdateDeleteUIState();
        }

        private enum UpdateDecision
        {
            AskEach,
            YesToAll,
            Yes,
            Skip,
            SkipAll
        }

        private static UpdateDecision ShowBulkUpdateDialog(string name)
        {
            using var dlg = new Form();
            dlg.Text = "Confirm Update";
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
            dlg.MinimizeBox = false;
            dlg.MaximizeBox = false;
            dlg.ShowInTaskbar = false;
            dlg.ClientSize = new Size(420, 150);

            var label = new Label
            {
                AutoSize = false,
                Text = $"'{name}' exists. What would you like to do?",
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(12, 12),
                Size = new Size(396, 60)
            };
            dlg.Controls.Add(label);

            UpdateDecision result = UpdateDecision.Skip;

            var btnYesAll = new Button { Text = "Update all", Size = new Size(90, 28), Location = new Point(12, 100) };
            btnYesAll.Click += (s, e) => { result = UpdateDecision.YesToAll; dlg.Close(); };
            dlg.Controls.Add(btnYesAll);

            var btnYes = new Button { Text = "Update " + name, Size = new Size(80, 28), Location = new Point(112, 100) };
            btnYes.Click += (s, e) => { result = UpdateDecision.Yes; dlg.Close(); };
            dlg.Controls.Add(btnYes);

            var btnSkip = new Button { Text = "Skip", Size = new Size(80, 28), Location = new Point(202, 100) };
            btnSkip.Click += (s, e) => { result = UpdateDecision.Skip; dlg.Close(); };
            dlg.Controls.Add(btnSkip);

            var btnSkipAll = new Button { Text = "Skip all", Size = new Size(90, 28), Location = new Point(292, 100) };
            btnSkipAll.Click += (s, e) => { result = UpdateDecision.SkipAll; dlg.Close(); };
            dlg.Controls.Add(btnSkipAll);

            dlg.AcceptButton = btnYes;
            dlg.CancelButton = btnSkip;

            dlg.ShowDialog();
            return result;
        }
    }
}


