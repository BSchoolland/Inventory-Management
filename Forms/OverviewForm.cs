using System.Media;
using Inventory_Management.Forms;

namespace Inventory_Management.Forms
{
    public partial class OverviewForm : Form
    {
        private InventoryViewForm inventoryViewForm;
        private ManageItemsForm manageItemsForm;
        private AddStockForm addStockForm;
        private NavigationControl nav;

        public OverviewForm()
        {
            InitializeComponent();
            inventoryViewForm = new InventoryViewForm(this);
            manageItemsForm = new ManageItemsForm(this, inventoryViewForm);
            addStockForm = new AddStockForm(this, inventoryViewForm, manageItemsForm);
            this.FormClosed += (s, e) => Application.Exit();

            nav = new NavigationControl(NavigationControl.NavigationPage.Overview);
            nav.Location = new Point(0, 0);
            Controls.Add(nav);
            

            nav.OverviewClicked += (s, e) => button1_Click(this, e);
            nav.ViewInventoryClicked += (s, e) => button2_Click(this, e);
            nav.ManageItemsClicked += (s, e) => button4_Click(this, e);
            nav.AddStockClicked += (s, e) => { addStockForm.Show(); this.Hide(); };
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
            inventoryViewForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            manageItemsForm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

