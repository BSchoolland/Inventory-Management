using System.Media;
using System.Text;
using Inventory_Management.Services;
using Inventory_Management.Models;

namespace Inventory_Management.Forms
{
    public partial class ManageItemsForm : Form
    {
        private OverviewForm overviewForm;
        private ManageInventoryForm manageInventoryForm;
        private NavigationControl nav;
        private List<string> cachedItemNames = new();

        public ManageItemsForm(OverviewForm parentOverview, ManageInventoryForm parentInventoryView)
        {
            overviewForm = parentOverview;
            manageInventoryForm = parentInventoryView;
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();

            nav = new NavigationControl(NavigationControl.NavigationPage.ManageItems);
            nav.Location = new Point(0, 0);
            Controls.Add(nav);

            nav.OverviewClicked += (s, e) => button1_Click(this, e);
            nav.ManageInventoryClicked += (s, e) => button2_Click(this, e);
            nav.ManageItemsClicked += (s, e) => button4_Click(this, e);
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            // Overview -> go to OverviewForm
            overviewForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // View Inventory -> go to ManageInventoryForm
            manageInventoryForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e) { }

        private void button4_Click(object sender, EventArgs e)
        {
            // Manage Items -> already here
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

                // Load all existing items ONCE instead of searching per item
                var existingItems = InventoryStorageSqlite.LoadItems();
                var existingDict = existingItems.ToDictionary(
                    i => i.Name.ToLower(), 
                    i => i, 
                    StringComparer.OrdinalIgnoreCase
                );

                int added = 0, updated = 0, skipped = 0, errors = 0;
                var bulkChoice = UpdateDecision.AskEach;
                var itemsToAdd = new List<InventoryItem>();
                var itemsToUpdate = new List<InventoryItem>();
                
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

                    // Check in dictionary instead of querying database
                    if (existingDict.TryGetValue(name, out var existing))
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
                            itemsToUpdate.Add(existing);
                            updated++;
                        }
                        else if (decision == UpdateDecision.Skip || decision == UpdateDecision.SkipAll)
                        {
                            skipped++;
                        }
                    }
                    else
                    {
                        var newItem = new InventoryItem
                        {
                            Name = name,
                            Description = string.Empty,
                            CurrentPrice = price,
                            StockQuantity = qty,
                            Barcode = string.Empty
                        };
                        itemsToAdd.Add(newItem);
                        added++;
                    }
                }

                // Batch insert/update all items
                if (itemsToAdd.Count > 0)
                {
                    InventoryStorageSqlite.BulkAddItems(itemsToAdd);
                }
                if (itemsToUpdate.Count > 0)
                {
                    InventoryStorageSqlite.BulkUpdateItems(itemsToUpdate);
                }

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
                var items = InventoryStorageSqlite.LoadItems();
                var name = deleteNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Enter a name to delete.");
                    return;
                }

                var toRemove = items.Where(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase)).ToList();
                foreach (var item in toRemove)
                {
                    InventoryStorageSqlite.DeleteItem(item);
                }

                if (toRemove.Count > 0)
                {
                    MessageBox.Show($"Deleted {toRemove.Count} item(s) named '{name}'.");
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
                string name = nameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Name is required."); return; }
                if (!decimal.TryParse(priceTextBox.Text.Trim(), out var price)) { MessageBox.Show("Invalid price."); return; }
                if (!int.TryParse(qtyTextBox.Text.Trim(), out var qty)) { MessageBox.Show("Invalid quantity."); return; }

                var items = InventoryStorageSqlite.LoadItems();
                var existing = items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
                
                if (existing != null)
                {
                    var resp = MessageBox.Show($"'{name}' exists. Update price/quantity?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resp == DialogResult.Yes)
                    {
                        existing.CurrentPrice = price;
                        existing.StockQuantity = qty;
                        InventoryStorageSqlite.UpdateItem(existing);
                    }
                    else
                    {
                        MessageBox.Show("Add cancelled.");
                        return;
                    }
                }
                else
                {
                    var newItem = new InventoryItem
                    {
                        Name = name,
                        Description = string.Empty,
                        CurrentPrice = price,
                        StockQuantity = qty,
                        Barcode = string.Empty
                    };
                    InventoryStorageSqlite.AddItem(newItem);
                }

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
                // Load first page (100 items) for name suggestions
                var firstPageItems = InventoryStorageSqlite.LoadItemsPaged(1, 100);
                var list = firstPageItems.Select(i => i.Name).Where(n => !string.IsNullOrWhiteSpace(n)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
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
