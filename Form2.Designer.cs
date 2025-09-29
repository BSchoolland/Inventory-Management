namespace Inventory_Management
{
    partial class Form2
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
            button1 = new Button();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            InventoryNavButton = new Button();
            panelInventory = new Panel();
            buttonForward = new Button();
            buttonBackward = new Button();
            searchTextBox = new TextBox();
            priceMinUpDown = new NumericUpDown();
            priceMaxUpDown = new NumericUpDown();
            stockMinUpDown = new NumericUpDown();
            stockMaxUpDown = new NumericUpDown();
            priceLabel = new Label();
            stockLabel = new Label();
            clearFiltersButton = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(button7);
            groupBox1.Controls.Add(button6);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(InventoryNavButton);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(139, 680);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Navigation";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // button1
            // 
            button1.Location = new Point(7, 26);
            button1.Name = "button1";
            button1.Size = new Size(125, 29);
            button1.TabIndex = 8;
            button1.Text = "Overview";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
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
            InventoryNavButton.BackColor = SystemColors.ActiveCaption;
            InventoryNavButton.Location = new Point(7, 61);
            InventoryNavButton.Name = "InventoryNavButton";
            InventoryNavButton.Size = new Size(125, 29);
            InventoryNavButton.TabIndex = 2;
            InventoryNavButton.Text = "View Inventory";
            InventoryNavButton.UseVisualStyleBackColor = false;
            InventoryNavButton.Click += button2_Click;
            // 
            // panelInventory
            // 
            panelInventory.AutoScroll = true;
            panelInventory.Location = new Point(160, 40);
            panelInventory.Name = "panelInventory";
            panelInventory.Size = new Size(600, 463);
            panelInventory.TabIndex = 10;
            // 
            // buttonForward
            // 
            buttonForward.Location = new Point(674, 509);
            buttonForward.Name = "buttonForward";
            buttonForward.Size = new Size(40, 30);
            buttonForward.TabIndex = 11;
            buttonForward.Text = ">";
            buttonForward.UseVisualStyleBackColor = true;
            buttonForward.Click += buttonForward_Click;
            // 
            // buttonBackward
            // 
            buttonBackward.Location = new Point(624, 509);
            buttonBackward.Name = "buttonBackward";
            buttonBackward.Size = new Size(40, 30);
            buttonBackward.TabIndex = 12;
            buttonBackward.Text = "<";
            buttonBackward.UseVisualStyleBackColor = true;
            buttonBackward.Click += buttonBackward_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(160, 10);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Search by name...";
            searchTextBox.Size = new Size(200, 27);
            searchTextBox.TabIndex = 13;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // priceMinUpDown
            // 
            priceMinUpDown.Location = new Point(437, 7);
            priceMinUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            priceMinUpDown.Name = "priceMinUpDown";
            priceMinUpDown.Size = new Size(60, 27);
            priceMinUpDown.TabIndex = 15;
            priceMinUpDown.ValueChanged += filter_ValueChanged;
            priceMinUpDown.TextChanged += filter_TextChanged;
            // 
            // priceMaxUpDown
            // 
            priceMaxUpDown.Location = new Point(503, 7);
            priceMaxUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            priceMaxUpDown.Name = "priceMaxUpDown";
            priceMaxUpDown.Size = new Size(60, 27);
            priceMaxUpDown.TabIndex = 16;
            priceMaxUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            priceMaxUpDown.ValueChanged += filter_ValueChanged;
            priceMaxUpDown.TextChanged += filter_TextChanged;
            // 
            // stockMinUpDown
            // 
            stockMinUpDown.Location = new Point(644, 7);
            stockMinUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            stockMinUpDown.Name = "stockMinUpDown";
            stockMinUpDown.Size = new Size(50, 27);
            stockMinUpDown.TabIndex = 18;
            stockMinUpDown.ValueChanged += filter_ValueChanged;
            stockMinUpDown.TextChanged += filter_TextChanged;
            // 
            // stockMaxUpDown
            // 
            stockMaxUpDown.Location = new Point(700, 7);
            stockMaxUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            stockMaxUpDown.Name = "stockMaxUpDown";
            stockMaxUpDown.Size = new Size(50, 27);
            stockMaxUpDown.TabIndex = 19;
            stockMaxUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            stockMaxUpDown.ValueChanged += filter_ValueChanged;
            stockMaxUpDown.TextChanged += filter_TextChanged;
            // 
            // priceLabel
            // 
            priceLabel.AutoSize = true;
            priceLabel.Location = new Point(370, 10);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new Size(66, 20);
            priceLabel.TabIndex = 14;
            priceLabel.Text = "Price ($):";
            // 
            // stockLabel
            // 
            stockLabel.AutoSize = true;
            stockLabel.Location = new Point(590, 10);
            stockLabel.Name = "stockLabel";
            stockLabel.Size = new Size(48, 20);
            stockLabel.TabIndex = 17;
            stockLabel.Text = "Stock:";
            // 
            // clearFiltersButton
            // 
            clearFiltersButton.Location = new Point(160, 509);
            clearFiltersButton.Name = "clearFiltersButton";
            clearFiltersButton.Size = new Size(120, 30);
            clearFiltersButton.TabIndex = 20;
            clearFiltersButton.Text = "Clear Filters";
            clearFiltersButton.UseVisualStyleBackColor = true;
            clearFiltersButton.Click += clearFiltersButton_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 556);
            Controls.Add(stockMaxUpDown);
            Controls.Add(stockMinUpDown);
            Controls.Add(stockLabel);
            Controls.Add(priceMaxUpDown);
            Controls.Add(priceMinUpDown);
            Controls.Add(priceLabel);
            Controls.Add(searchTextBox);
            Controls.Add(buttonBackward);
            Controls.Add(buttonForward);
            Controls.Add(panelInventory);
            Controls.Add(groupBox1);
            Controls.Add(clearFiltersButton);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Button InventoryNavButton;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button1;
        private Panel panelInventory;
        private Button buttonForward;
        private Button buttonBackward;
        private TextBox searchTextBox;
        private NumericUpDown priceMinUpDown;
        private NumericUpDown priceMaxUpDown;
        private NumericUpDown stockMinUpDown;
        private NumericUpDown stockMaxUpDown;
        private Label priceLabel;
        private Label stockLabel;
        private Button clearFiltersButton;
    }
}
