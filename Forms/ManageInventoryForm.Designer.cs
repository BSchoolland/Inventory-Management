namespace Inventory_Management.Forms
{
    partial class ManageInventoryForm
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
            itemCountLabel = new Label();
            pageInfoLabel = new Label();
            addItemGroupBox = new GroupBox();
            nameLabel = new Label();
            nameTextBox = new TextBox();
            descriptionLabel = new Label();
            descriptionTextBox = new TextBox();
            priceGroupLabel = new Label();
            priceTextBox = new TextBox();
            qtyLabel = new Label();
            qtyTextBox = new TextBox();
            addButton = new Button();
            stockAdjustGroupBox = new GroupBox();
            incButton = new Button();
            decButton = new Button();
            incQtyLabel = new Label();
            incQtyTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewInventory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).BeginInit();
            addItemGroupBox.SuspendLayout();
            stockAdjustGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewInventory
            // 
            dataGridViewInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewInventory.Location = new Point(161, 40);
            dataGridViewInventory.Name = "dataGridViewInventory";
            dataGridViewInventory.ReadOnly = true;
            dataGridViewInventory.RowHeadersWidth = 51;
            dataGridViewInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewInventory.Size = new Size(874, 300);
            dataGridViewInventory.TabIndex = 10;
            // 
            // buttonForward
            // 
            buttonForward.BackColor = Color.FromArgb(50, 50, 50);
            buttonForward.FlatStyle = FlatStyle.Flat;
            buttonForward.ForeColor = Color.White;
            buttonForward.Location = new Point(997, 351);
            buttonForward.Name = "buttonForward";
            buttonForward.Size = new Size(40, 30);
            buttonForward.TabIndex = 11;
            buttonForward.Text = ">";
            buttonForward.UseVisualStyleBackColor = false;
            buttonForward.Click += buttonForward_Click;
            // 
            // buttonBackward
            // 
            buttonBackward.BackColor = Color.FromArgb(50, 50, 50);
            buttonBackward.FlatStyle = FlatStyle.Flat;
            buttonBackward.ForeColor = Color.White;
            buttonBackward.Location = new Point(947, 351);
            buttonBackward.Name = "buttonBackward";
            buttonBackward.Size = new Size(40, 30);
            buttonBackward.TabIndex = 12;
            buttonBackward.Text = "<";
            buttonBackward.UseVisualStyleBackColor = false;
            buttonBackward.Click += buttonBackward_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.BackColor = Color.FromArgb(50, 50, 50);
            searchTextBox.BorderStyle = BorderStyle.FixedSingle;
            searchTextBox.ForeColor = Color.White;
            searchTextBox.Location = new Point(160, 10);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Search by name...";
            searchTextBox.Size = new Size(200, 27);
            searchTextBox.TabIndex = 13;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // priceMinUpDown
            // 
            priceMinUpDown.BackColor = Color.FromArgb(50, 50, 50);
            priceMinUpDown.ForeColor = Color.White;
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
            priceMaxUpDown.BackColor = Color.FromArgb(50, 50, 50);
            priceMaxUpDown.ForeColor = Color.White;
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
            stockMinUpDown.BackColor = Color.FromArgb(50, 50, 50);
            stockMinUpDown.ForeColor = Color.White;
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
            stockMaxUpDown.BackColor = Color.FromArgb(50, 50, 50);
            stockMaxUpDown.ForeColor = Color.White;
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
            clearFiltersButton.BackColor = Color.FromArgb(50, 50, 50);
            clearFiltersButton.FlatStyle = FlatStyle.Flat;
            clearFiltersButton.ForeColor = Color.White;
            clearFiltersButton.Location = new Point(160, 346);
            clearFiltersButton.Name = "clearFiltersButton";
            clearFiltersButton.Size = new Size(120, 30);
            clearFiltersButton.TabIndex = 20;
            clearFiltersButton.Text = "Clear Filters";
            clearFiltersButton.UseVisualStyleBackColor = false;
            clearFiltersButton.Click += clearFiltersButton_Click;
            // 
            // itemCountLabel
            // 
            itemCountLabel.AutoSize = true;
            itemCountLabel.Location = new Point(286, 351);
            itemCountLabel.Name = "itemCountLabel";
            itemCountLabel.Size = new Size(159, 20);
            itemCountLabel.TabIndex = 21;
            itemCountLabel.Text = "Showing: 10000/10000";
            // 
            // pageInfoLabel
            // 
            pageInfoLabel.AutoSize = true;
            pageInfoLabel.Location = new Point(816, 356);
            pageInfoLabel.Name = "pageInfoLabel";
            pageInfoLabel.Size = new Size(83, 20);
            pageInfoLabel.TabIndex = 22;
            pageInfoLabel.Text = "Page 1/100";
            // 
            // addItemGroupBox
            // 
            addItemGroupBox.Controls.Add(nameLabel);
            addItemGroupBox.Controls.Add(nameTextBox);
            addItemGroupBox.Controls.Add(descriptionLabel);
            addItemGroupBox.Controls.Add(descriptionTextBox);
            addItemGroupBox.Controls.Add(priceGroupLabel);
            addItemGroupBox.Controls.Add(priceTextBox);
            addItemGroupBox.Controls.Add(qtyLabel);
            addItemGroupBox.Controls.Add(qtyTextBox);
            addItemGroupBox.Controls.Add(addButton);
            addItemGroupBox.ForeColor = Color.White;
            addItemGroupBox.Location = new Point(160, 390);
            addItemGroupBox.Name = "addItemGroupBox";
            addItemGroupBox.Size = new Size(499, 150);
            addItemGroupBox.TabIndex = 23;
            addItemGroupBox.TabStop = false;
            addItemGroupBox.Text = "Add New Item";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(10, 25);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(52, 20);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name:";
            // 
            // nameTextBox
            // 
            nameTextBox.BackColor = Color.FromArgb(50, 50, 50);
            nameTextBox.BorderStyle = BorderStyle.FixedSingle;
            nameTextBox.ForeColor = Color.White;
            nameTextBox.Location = new Point(101, 22);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Item name";
            nameTextBox.Size = new Size(150, 27);
            nameTextBox.TabIndex = 1;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(11, 61);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(88, 20);
            descriptionLabel.TabIndex = 2;
            descriptionLabel.Text = "Description:";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.BackColor = Color.FromArgb(50, 50, 50);
            descriptionTextBox.BorderStyle = BorderStyle.FixedSingle;
            descriptionTextBox.ForeColor = Color.White;
            descriptionTextBox.Location = new Point(101, 58);
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.PlaceholderText = "Item description";
            descriptionTextBox.Size = new Size(316, 27);
            descriptionTextBox.TabIndex = 3;
            // 
            // priceGroupLabel
            // 
            priceGroupLabel.AutoSize = true;
            priceGroupLabel.Location = new Point(10, 99);
            priceGroupLabel.Name = "priceGroupLabel";
            priceGroupLabel.Size = new Size(44, 20);
            priceGroupLabel.TabIndex = 4;
            priceGroupLabel.Text = "Price:";
            // 
            // priceTextBox
            // 
            priceTextBox.BackColor = Color.FromArgb(50, 50, 50);
            priceTextBox.BorderStyle = BorderStyle.FixedSingle;
            priceTextBox.ForeColor = Color.White;
            priceTextBox.Location = new Point(101, 96);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.PlaceholderText = "0.00";
            priceTextBox.Size = new Size(80, 27);
            priceTextBox.TabIndex = 5;
            // 
            // qtyLabel
            // 
            qtyLabel.AutoSize = true;
            qtyLabel.Location = new Point(190, 99);
            qtyLabel.Name = "qtyLabel";
            qtyLabel.Size = new Size(35, 20);
            qtyLabel.TabIndex = 6;
            qtyLabel.Text = "Qty:";
            // 
            // qtyTextBox
            // 
            qtyTextBox.BackColor = Color.FromArgb(50, 50, 50);
            qtyTextBox.BorderStyle = BorderStyle.FixedSingle;
            qtyTextBox.ForeColor = Color.White;
            qtyTextBox.Location = new Point(230, 96);
            qtyTextBox.Name = "qtyTextBox";
            qtyTextBox.PlaceholderText = "0";
            qtyTextBox.Size = new Size(60, 27);
            qtyTextBox.TabIndex = 7;
            // 
            // addButton
            // 
            addButton.BackColor = Color.FromArgb(50, 50, 50);
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(335, 110);
            addButton.Name = "addButton";
            addButton.Size = new Size(100, 29);
            addButton.TabIndex = 8;
            addButton.Text = "Add Item";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            // 
            // stockAdjustGroupBox
            // 
            stockAdjustGroupBox.Controls.Add(incButton);
            stockAdjustGroupBox.Controls.Add(decButton);
            stockAdjustGroupBox.Controls.Add(incQtyLabel);
            stockAdjustGroupBox.Controls.Add(incQtyTextBox);
            stockAdjustGroupBox.ForeColor = Color.White;
            stockAdjustGroupBox.Location = new Point(665, 390);
            stockAdjustGroupBox.Name = "stockAdjustGroupBox";
            stockAdjustGroupBox.Size = new Size(372, 90);
            stockAdjustGroupBox.TabIndex = 24;
            stockAdjustGroupBox.TabStop = false;
            stockAdjustGroupBox.Text = "Adjust Stock (select item above)";
            // 
            // incButton
            // 
            incButton.BackColor = Color.FromArgb(50, 50, 50);
            incButton.FlatStyle = FlatStyle.Flat;
            incButton.ForeColor = Color.White;
            incButton.Location = new Point(15, 45);
            incButton.Name = "incButton";
            incButton.Size = new Size(85, 29);
            incButton.TabIndex = 0;
            incButton.Text = "Increment";
            incButton.UseVisualStyleBackColor = false;
            incButton.Click += incButton_Click;
            // 
            // decButton
            // 
            decButton.BackColor = Color.FromArgb(50, 50, 50);
            decButton.FlatStyle = FlatStyle.Flat;
            decButton.ForeColor = Color.White;
            decButton.Location = new Point(106, 45);
            decButton.Name = "decButton";
            decButton.Size = new Size(90, 29);
            decButton.TabIndex = 1;
            decButton.Text = "Decrement";
            decButton.UseVisualStyleBackColor = false;
            decButton.Click += decButton_Click;
            // 
            // incQtyLabel
            // 
            incQtyLabel.AutoSize = true;
            incQtyLabel.Location = new Point(276, 49);
            incQtyLabel.Name = "incQtyLabel";
            incQtyLabel.Size = new Size(35, 20);
            incQtyLabel.TabIndex = 2;
            incQtyLabel.Text = "Qty:";
            // 
            // incQtyTextBox
            // 
            incQtyTextBox.BackColor = Color.FromArgb(50, 50, 50);
            incQtyTextBox.BorderStyle = BorderStyle.FixedSingle;
            incQtyTextBox.ForeColor = Color.White;
            incQtyTextBox.Location = new Point(316, 46);
            incQtyTextBox.Name = "incQtyTextBox";
            incQtyTextBox.PlaceholderText = "1";
            incQtyTextBox.Size = new Size(50, 27);
            incQtyTextBox.TabIndex = 3;
            incQtyTextBox.Text = "1";
            // 
            // ManageInventoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 34, 34);
            ClientSize = new Size(1047, 590);
            Controls.Add(stockAdjustGroupBox);
            Controls.Add(addItemGroupBox);
            Controls.Add(pageInfoLabel);
            Controls.Add(itemCountLabel);
            Controls.Add(stockMaxUpDown);
            Controls.Add(stockMinUpDown);
            Controls.Add(stockLabel);
            Controls.Add(priceMaxUpDown);
            Controls.Add(priceMinUpDown);
            Controls.Add(clearFiltersButton);
            Controls.Add(priceLabel);
            Controls.Add(searchTextBox);
            Controls.Add(buttonBackward);
            Controls.Add(buttonForward);
            Controls.Add(dataGridViewInventory);
            ForeColor = Color.White;
            Name = "ManageInventoryForm";
            Text = "Manage Inventory";
            Load += ManageInventoryForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewInventory).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).EndInit();
            addItemGroupBox.ResumeLayout(false);
            addItemGroupBox.PerformLayout();
            stockAdjustGroupBox.ResumeLayout(false);
            stockAdjustGroupBox.PerformLayout();
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
        private Label itemCountLabel;
        private Label pageInfoLabel;
        private GroupBox addItemGroupBox;
        private Label nameLabel;
        private TextBox nameTextBox;
        private Label descriptionLabel;
        private TextBox descriptionTextBox;
        private Label priceGroupLabel;
        private TextBox priceTextBox;
        private Label qtyLabel;
        private TextBox qtyTextBox;
        private Button addButton;
        private GroupBox stockAdjustGroupBox;
        private Button incButton;
        private Button decButton;
        private Label incQtyLabel;
        private TextBox incQtyTextBox;
    }
}

