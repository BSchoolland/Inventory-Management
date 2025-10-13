using System.Media;

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
    }
}


