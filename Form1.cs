using System.Media;

namespace Inventory_Management
{
    public partial class Form1 : Form
    {
        private Form2 form2;
        private Form3 form3;
        private NavigationControl nav;

        public Form1()
        {
            InitializeComponent();
            form2 = new Form2(this);
            form3 = new Form3(this, form2);
            this.FormClosed += (s, e) => Application.Exit();

            nav = new NavigationControl();
            nav.Location = new Point(0, 0);
            Controls.Add(nav);
            if (groupBox1 != null) groupBox1.Visible = false;

            nav.OverviewClicked += (s, e) => button1_Click(s, e);
            nav.ViewInventoryClicked += (s, e) => button2_Click(s, e);
            nav.ManageItemsClicked += (s, e) => button4_Click(s, e);
            nav.AddStockClicked += (s, e) => SystemSounds.Beep.Play();
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
            // Show Form2 and hide Form1
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            form3.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
