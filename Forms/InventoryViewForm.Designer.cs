namespace Inventory_Management.Forms
{
    partial class InventoryViewForm
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
            dataGridViewInventory = new DataGridView();
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
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInventory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewInventory
            // 
            dataGridViewInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInventory.Location = new Point(160, 40);
            dataGridViewInventory.Name = "dataGridViewInventory";
            dataGridViewInventory.ReadOnly = true;
            dataGridViewInventory.RowHeadersWidth = 51;
            dataGridViewInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewInventory.Size = new Size(1241, 463);
            dataGridViewInventory.TabIndex = 10;
            // 
            // buttonForward
            // 
            buttonForward.Location = new Point(1361, 509);
            buttonForward.Name = "buttonForward";
            buttonForward.Size = new Size(40, 30);
            buttonForward.TabIndex = 11;
            buttonForward.Text = ">";
            buttonForward.UseVisualStyleBackColor = true;
            buttonForward.Click += buttonForward_Click;
            // 
            // buttonBackward
            // 
            buttonBackward.Location = new Point(1311, 509);
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
            priceMinUpDown.Location = new Point(1109, 7);
            priceMinUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            priceMinUpDown.Name = "priceMinUpDown";
            priceMinUpDown.Size = new Size(60, 27);
            priceMinUpDown.TabIndex = 15;
            priceMinUpDown.TextChanged += filter_TextChanged;
            priceMinUpDown.ValueChanged += filter_ValueChanged;
            // 
            // priceMaxUpDown
            // 
            priceMaxUpDown.Location = new Point(1175, 7);
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
            stockMinUpDown.Location = new Point(1295, 7);
            stockMinUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            stockMinUpDown.Name = "stockMinUpDown";
            stockMinUpDown.Size = new Size(50, 27);
            stockMinUpDown.TabIndex = 18;
            stockMinUpDown.TextChanged += filter_TextChanged;
            stockMinUpDown.ValueChanged += filter_ValueChanged;
            // 
            // stockMaxUpDown
            // 
            stockMaxUpDown.Location = new Point(1351, 7);
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
            priceLabel.Location = new Point(1042, 10);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new Size(66, 20);
            priceLabel.TabIndex = 14;
            priceLabel.Text = "Price ($):";
            // 
            // stockLabel
            // 
            stockLabel.AutoSize = true;
            stockLabel.Location = new Point(1241, 10);
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(286, 514);
            label1.Name = "label1";
            label1.Size = new Size(159, 20);
            label1.TabIndex = 21;
            label1.Text = "Showing: 10000/10000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1193, 514);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 22;
            label2.Text = "Page 1/100";
            label2.Click += label2_Click_1;
            // 
            // InventoryViewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1518, 556);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(stockMaxUpDown);
            Controls.Add(stockMinUpDown);
            Controls.Add(stockLabel);
            Controls.Add(priceMaxUpDown);
            Controls.Add(priceMinUpDown);
            Controls.Add(priceLabel);
            Controls.Add(searchTextBox);
            Controls.Add(buttonBackward);
            Controls.Add(buttonForward);
            Controls.Add(dataGridViewInventory);
            Controls.Add(clearFiltersButton);
            Name = "InventoryViewForm";
            Text = "View Inventory";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewInventory).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridViewInventory;
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
        private Label label1;
        private Label label2;
    }
}
