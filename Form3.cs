using System.Media;
using System.Text;

namespace Inventory_Management
{
    public partial class Form3 : Form
    {
        private Form1 form1;
        private Form2 form2;
        private NavigationControl nav;
		private List<string> cachedItemNames = new();
        private Form4 form4;

        public Form3(Form1 parent1, Form2 parent2)
        {
            form1 = parent1;
            form2 = parent2;
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();

            nav = new NavigationControl(NavigationControl.NavigationPage.ManageItems);
            nav.Location = new Point(0, 0);
            Controls.Add(nav);
            form4 = new Form4(form1, form2, this);


            nav.OverviewClicked += (s, e) => button1_Click(s, e);
            nav.ViewInventoryClicked += (s, e) => button2_Click(s, e);
            nav.ManageItemsClicked += (s, e) => button4_Click(s, e);
            nav.AddStockClicked += (s, e) => { form4.Show(); this.Hide(); };
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

                InventoryStorage.EnsureDataFile();
                var items = InventoryStorage.LoadItems();

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

                    var existing = items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
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
                        items.Add(new InventoryItem
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

				InventoryStorage.SaveItems(items);
				MessageBox.Show($"Added: {added}\nUpdated: {updated}\nSkipped: {skipped}\nErrors: {errors}", "Upload Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
				RefreshItemNames();
				UpdateDeleteUIState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to upload items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var items = InventoryStorage.LoadItems();
                var name = deleteNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Enter a name to delete.");
                    return;
                }

                var removed = items.RemoveAll(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
                if (removed > 0)
                {
                    InventoryStorage.SaveItems(items);
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
                InventoryStorage.EnsureDataFile();
                var items = InventoryStorage.LoadItems();

                string name = nameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Name is required."); return; }
                if (!decimal.TryParse(priceTextBox.Text.Trim(), out var price)) { MessageBox.Show("Invalid price."); return; }
                if (!int.TryParse(qtyTextBox.Text.Trim(), out var qty)) { MessageBox.Show("Invalid quantity."); return; }

                var existing = items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
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
                    items.Add(new InventoryItem
                    {
                        Name = name,
                        Description = string.Empty,
                        CurrentPrice = price,
                        StockQuantity = qty,
                        Barcode = string.Empty
                    });
                }

				InventoryStorage.SaveItems(items);
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
                var list = InventoryStorage.LoadItems().Select(i => i.Name).Where(n => !string.IsNullOrWhiteSpace(n)).ToList();
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


