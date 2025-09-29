namespace Inventory_Management
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            InventoryNavButton = new Button();
            OverviewNavButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button7);
            groupBox1.Controls.Add(button6);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(InventoryNavButton);
            groupBox1.Controls.Add(OverviewNavButton);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(139, 452);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Navigation";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // button7
            // 
            button7.BackColor = SystemColors.ControlDarkDark;
            button7.Location = new Point(7, 236);
            button7.Name = "button7";
            button7.Size = new Size(125, 29);
            button7.TabIndex = 7;
            button7.Text = "Checkout";
            button7.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ControlDarkDark;
            button6.Location = new Point(7, 201);
            button6.Name = "button6";
            button6.Size = new Size(125, 29);
            button6.TabIndex = 6;
            button6.Text = "Projections";
            button6.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ControlDarkDark;
            button5.Location = new Point(7, 166);
            button5.Name = "button5";
            button5.Size = new Size(125, 29);
            button5.TabIndex = 5;
            button5.Text = "Edit Item";
            button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ControlDarkDark;
            button4.Location = new Point(7, 131);
            button4.Name = "button4";
            button4.Size = new Size(125, 29);
            button4.TabIndex = 4;
            button4.Text = "Create Item";
            button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ControlDarkDark;
            button3.Location = new Point(7, 96);
            button3.Name = "button3";
            button3.RightToLeft = RightToLeft.Yes;
            button3.Size = new Size(125, 29);
            button3.TabIndex = 3;
            button3.Text = "Add Stock";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // InventoryNavButton
            // 
            InventoryNavButton.Location = new Point(7, 61);
            InventoryNavButton.Name = "InventoryNavButton";
            InventoryNavButton.Size = new Size(125, 29);
            InventoryNavButton.TabIndex = 2;
            InventoryNavButton.Text = "View Inventory";
            InventoryNavButton.UseVisualStyleBackColor = true;
            InventoryNavButton.Click += button2_Click;
            // 
            // OverviewNavButton
            // 
            OverviewNavButton.BackColor = SystemColors.ActiveCaption;
            OverviewNavButton.FlatStyle = FlatStyle.Popup;
            OverviewNavButton.ForeColor = SystemColors.ActiveCaptionText;
            OverviewNavButton.Location = new Point(7, 26);
            OverviewNavButton.Name = "OverviewNavButton";
            OverviewNavButton.Size = new Size(125, 29);
            OverviewNavButton.TabIndex = 1;
            OverviewNavButton.Text = "Overview";
            OverviewNavButton.UseVisualStyleBackColor = false;
            OverviewNavButton.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(393, 9);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 1;
            label1.Text = "Overview";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(356, 96);
            label2.Name = "label2";
            label2.Size = new Size(141, 20);
            label2.TabIndex = 2;
            label2.Text = "Info Would Go Here";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(356, 245);
            label3.Name = "label3";
            label3.Size = new Size(135, 20);
            label3.TabIndex = 3;
            label3.Text = "Suggested Actions:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(302, 302);
            label4.Name = "label4";
            label4.Size = new Size(238, 20);
            label4.TabIndex = 4;
            label4.Text = "Suggested Actions Would Go Here";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Button InventoryNavButton;
        private Button OverviewNavButton;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
