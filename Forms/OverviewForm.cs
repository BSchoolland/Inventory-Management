using System.Media;
using Inventory_Management.Forms;
using Inventory_Management.Services;

namespace Inventory_Management.Forms
{
    public partial class OverviewForm : Form
    {
        private InventoryViewForm? inventoryViewForm;
        private ManageItemsForm? manageItemsForm;
        private AddStockForm? addStockForm;
        private NavigationControl nav;

        public OverviewForm()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();

            nav = new NavigationControl(NavigationControl.NavigationPage.Overview);
            nav.Location = new Point(0, 0);
            Controls.Add(nav);
            

            nav.OverviewClicked += (s, e) => button1_Click(this, e);
            nav.ViewInventoryClicked += (s, e) => button2_Click(this, e);
            nav.ManageItemsClicked += (s, e) => button4_Click(this, e);
            nav.AddStockClicked += (s, e) => { EnsureAddStockForm().Show(); this.Hide(); };
            nav.ProjectionsClicked += (s, e) => SystemSounds.Beep.Play();
            nav.CheckoutClicked += (s, e) => SystemSounds.Beep.Play();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // play error sound, we can't go to the page we're already on
            SystemSounds.Hand.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Show InventoryViewForm and hide OverviewForm
            EnsureInventoryViewForm().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            EnsureManageItemsForm().Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadInventoryStats();
            
            // Start loading inventory data in the background after the form is shown
            // This ensures data is ready when user navigates to inventory view
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    EnsureInventoryViewForm().InitializeInventory();
                }
                catch
                {
                    // Silently fail if there's an error loading inventory data
                    // The user can still navigate and see the error then
                }
            }));
        }

        /// <summary>
        /// Lazily initializes and returns the InventoryViewForm.
        /// </summary>
        private InventoryViewForm EnsureInventoryViewForm()
        {
            if (inventoryViewForm == null)
            {
                inventoryViewForm = new InventoryViewForm(this);
            }
            return inventoryViewForm;
        }

        /// <summary>
        /// Lazily initializes and returns the ManageItemsForm.
        /// </summary>
        private ManageItemsForm EnsureManageItemsForm()
        {
            if (manageItemsForm == null)
            {
                manageItemsForm = new ManageItemsForm(this, EnsureInventoryViewForm());
            }
            return manageItemsForm;
        }

        /// <summary>
        /// Lazily initializes and returns the AddStockForm.
        /// </summary>
        private AddStockForm EnsureAddStockForm()
        {
            if (addStockForm == null)
            {
                addStockForm = new AddStockForm(this, EnsureInventoryViewForm(), EnsureManageItemsForm());
            }
            return addStockForm;
        }

        /// <summary>
        /// Loads aggregate inventory statistics from the database and displays them
        /// on the overview page.
        /// </summary>
        private void LoadInventoryStats()
        {
            try
            {
                var stats = InventoryStorageSqlite.GetInventoryStats();

                if (stats.TotalItems == 0)
                {
                    label2.Text = "No items in the inventory yet.";
                    label3.Text = string.Empty;
                    label4.Text = string.Empty;
                    return;
                }

                label2.Text =
                    $"Total distinct items: {stats.TotalItems:N0}{Environment.NewLine}" +
                    $"Total units in stock: {stats.TotalUnitsInStock:N0}{Environment.NewLine}" +
                    $"Total inventory value: {stats.TotalInventoryValue:C2}";

                label3.Text = "Price & stock overview:";

                label4.Text =
                    $"Average price per item: {stats.AveragePrice:C2}{Environment.NewLine}" +
                    $"Lowest price: {stats.MinPrice:C2}{Environment.NewLine}" +
                    $"Highest price: {stats.MaxPrice:C2}{Environment.NewLine}" +
                    $"Average stock per item: {stats.AverageStockPerItem:N2} units";
            }
            catch (Exception ex)
            {
                label2.Text = "Failed to load inventory statistics.";
                label3.Text = string.Empty;
                label4.Text = ex.Message;
            }
        }
    }
}

