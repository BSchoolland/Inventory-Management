namespace Inventory_Management.Forms
{
    partial class AddStockForm
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
            addItemGroupBox = new GroupBox();
            nameLabel = new Label();
            nameTextBox = new TextBox();
            descriptionLabel = new Label();
            descriptionTextBox = new TextBox();
            priceGroupLabel = new Label();
            priceTextBox = new TextBox();
            qtyLabel = new Label();
            qtyTextBox = new TextBox();
            barcodeLabel = new Label();
            barcodeTextBox = new TextBox();
            addButton = new Button();
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
            dataGridViewInventory.Size = new Size(687, 250);
            dataGridViewInventory.TabIndex = 10;
            // 
            // buttonForward
            // 
            buttonForward.Location = new Point(758, 304);
            buttonForward.Name = "buttonForward";
            buttonForward.Size = new Size(40, 30);
            buttonForward.TabIndex = 11;
            buttonForward.Text = ">";
            buttonForward.UseVisualStyleBackColor = true;
            buttonForward.Visible = false;
            buttonForward.Click += buttonForward_Click;
            // 
            // buttonBackward
            // 
            buttonBackward.Location = new Point(700, 304);
            buttonBackward.Name = "buttonBackward";
            buttonBackward.Size = new Size(40, 30);
            buttonBackward.TabIndex = 12;
            buttonBackward.Text = "<";
            buttonBackward.UseVisualStyleBackColor = true;
            buttonBackward.Visible = false;
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
            clearFiltersButton.Location = new Point(160, 304);
            clearFiltersButton.Name = "clearFiltersButton";
            clearFiltersButton.Size = new Size(120, 30);
            clearFiltersButton.TabIndex = 20;
            clearFiltersButton.Text = "Clear Filters";
            clearFiltersButton.UseVisualStyleBackColor = true;
            clearFiltersButton.Click += clearFiltersButton_Click;
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
            addItemGroupBox.Controls.Add(barcodeLabel);
            addItemGroupBox.Controls.Add(barcodeTextBox);
            addItemGroupBox.Controls.Add(addButton);
            addItemGroupBox.Location = new Point(159, 400);
            addItemGroupBox.Name = "addItemGroupBox";
            addItemGroupBox.Size = new Size(600, 187);
            addItemGroupBox.TabIndex = 21;
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
            qtyTextBox.Location = new Point(230, 96);
            qtyTextBox.Name = "qtyTextBox";
            qtyTextBox.PlaceholderText = "0";
            qtyTextBox.Size = new Size(60, 27);
            qtyTextBox.TabIndex = 7;
            // 
            // barcodeLabel
            // 
            barcodeLabel.AutoSize = true;
            barcodeLabel.Location = new Point(10, 138);
            barcodeLabel.Name = "barcodeLabel";
            barcodeLabel.Size = new Size(67, 20);
            barcodeLabel.TabIndex = 8;
            barcodeLabel.Text = "Barcode:";
            // 
            // barcodeTextBox
            // 
            barcodeTextBox.Location = new Point(100, 135);
            barcodeTextBox.Name = "barcodeTextBox";
            barcodeTextBox.PlaceholderText = "Barcode";
            barcodeTextBox.Size = new Size(180, 27);
            barcodeTextBox.TabIndex = 9;
            // 
            // addButton
            // 
            addButton.Location = new Point(485, 150);
            addButton.Name = "addButton";
            addButton.Size = new Size(100, 29);
            addButton.TabIndex = 10;
            addButton.Text = "Add Item";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // incButton
            // 
            incButton.Location = new Point(162, 352);
            incButton.Name = "incButton";
            incButton.Size = new Size(98, 29);
            incButton.TabIndex = 21;
            incButton.Text = "Increment";
            incButton.UseVisualStyleBackColor = true;
            incButton.Click += incButton_Click;
            // 
            // decButton
            // 
            decButton.Location = new Point(266, 353);
            decButton.Name = "decButton";
            decButton.Size = new Size(105, 29);
            decButton.TabIndex = 22;
            decButton.Text = "Decrement";
            decButton.UseVisualStyleBackColor = true;
            decButton.Click += decButton_Click;
            // 
            // incQtyLabel
            // 
            incQtyLabel.AutoSize = true;
            incQtyLabel.Location = new Point(398, 357);
            incQtyLabel.Name = "incQtyLabel";
            incQtyLabel.Size = new Size(65, 20);
            incQtyLabel.TabIndex = 24;
            incQtyLabel.Text = "Amount:";
            // 
            // incQtyTextBox
            // 
            incQtyTextBox.Location = new Point(468, 354);
            incQtyTextBox.Name = "incQtyTextBox";
            incQtyTextBox.PlaceholderText = "1";
            incQtyTextBox.Size = new Size(60, 27);
            incQtyTextBox.TabIndex = 25;
            incQtyTextBox.Text = "1";
            // 
            // AddStockForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(859, 591);
            Controls.Add(incQtyTextBox);
            Controls.Add(incQtyLabel);
            Controls.Add(decButton);
            Controls.Add(incButton);
            Controls.Add(addItemGroupBox);
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
            Name = "AddStockForm";
            Text = "Add Stock";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewInventory).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceMaxUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMinUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMaxUpDown).EndInit();
            addItemGroupBox.ResumeLayout(false);
            addItemGroupBox.PerformLayout();
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
        private GroupBox addItemGroupBox;
        private Label nameLabel;
        private TextBox nameTextBox;
        private Label descriptionLabel;
        private TextBox descriptionTextBox;
        private Label priceGroupLabel;
        private TextBox priceTextBox;
        private Label qtyLabel;
        private TextBox qtyTextBox;
        private Label barcodeLabel;
        private TextBox barcodeTextBox;
        private Button addButton;
        private Button incButton;
        private Button decButton;
        private Label incQtyLabel;
        private TextBox incQtyTextBox;
    }
}


