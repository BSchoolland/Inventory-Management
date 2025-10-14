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
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).BeginInit();
            SuspendLayout();
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
            priceMinUpDown.TextChanged += filter_TextChanged;
            priceMinUpDown.ValueChanged += filter_ValueChanged;
            // 
            // priceMaxUpDown
            // 
            priceMaxUpDown.Location = new Point(503, 7);
            priceMaxUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            priceMaxUpDown.Name = "priceMaxUpDown";
            priceMaxUpDown.Size = new Size(60, 27);
            priceMaxUpDown.TabIndex = 16;
            priceMaxUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            priceMaxUpDown.TextChanged += filter_TextChanged;
            priceMaxUpDown.ValueChanged += filter_ValueChanged;
            // 
            // stockMinUpDown
            // 
            stockMinUpDown.Location = new Point(644, 7);
            stockMinUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            stockMinUpDown.Name = "stockMinUpDown";
            stockMinUpDown.Size = new Size(50, 27);
            stockMinUpDown.TabIndex = 18;
            stockMinUpDown.TextChanged += filter_TextChanged;
            stockMinUpDown.ValueChanged += filter_ValueChanged;
            // 
            // stockMaxUpDown
            // 
            stockMaxUpDown.Location = new Point(700, 7);
            stockMaxUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            stockMaxUpDown.Name = "stockMaxUpDown";
            stockMaxUpDown.Size = new Size(50, 27);
            stockMaxUpDown.TabIndex = 19;
            stockMaxUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            stockMaxUpDown.TextChanged += filter_TextChanged;
            stockMaxUpDown.ValueChanged += filter_ValueChanged;
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
            Controls.Add(clearFiltersButton);
            Name = "Form2";
            Text = "View Inventory";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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
