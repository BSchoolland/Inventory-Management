using System.Media;

namespace Inventory_Management
{
    public partial class Form1 : Form
    {
        private Form2 form2;

        public Form1()
        {
            InitializeComponent();
            form2 = new Form2(this);
            this.FormClosed += (s, e) => Application.Exit();
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
    }
}
