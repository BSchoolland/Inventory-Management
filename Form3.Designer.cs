namespace Inventory_Management
{
    partial class Form3
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
            label1 = new Label();
            uploadButton = new Button();
            label2 = new Label();
            deleteLabel = new Label();
            deleteNameTextBox = new TextBox();
            deleteButton = new Button();
            addLabel = new Label();
            nameTextBox = new TextBox();
            priceTextBox = new TextBox();
            qtyTextBox = new TextBox();
            addManualButton = new Button();
            label3 = new Label();
            deleteSuggestionsListBox = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(393, 9);
            label1.Name = "label1";
            label1.Size = new Size(103, 20);
            label1.TabIndex = 1;
            label1.Text = "Manage Items";
            // 
            // uploadButton
            // 
            uploadButton.Location = new Point(375, 80);
            uploadButton.Name = "uploadButton";
            uploadButton.Size = new Size(150, 30);
            uploadButton.TabIndex = 5;
            uploadButton.Text = "Upload Text File";
            uploadButton.UseVisualStyleBackColor = true;
            uploadButton.Click += uploadButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(395, 56);
            label2.Name = "label2";
            label2.Size = new Size(99, 20);
            label2.TabIndex = 6;
            label2.Text = "Item uploads:";
            label2.Click += label2_Click_1;
            // 
            // deleteLabel
            // 
            deleteLabel.AutoSize = true;
            deleteLabel.Location = new Point(306, 239);
            deleteLabel.Name = "deleteLabel";
            deleteLabel.Size = new Size(117, 20);
            deleteLabel.TabIndex = 7;
            deleteLabel.Text = "Delete by name:";
            // 
            // deleteNameTextBox
            // 
            deleteNameTextBox.Location = new Point(306, 262);
            deleteNameTextBox.Name = "deleteNameTextBox";
            deleteNameTextBox.Size = new Size(200, 27);
            deleteNameTextBox.TabIndex = 8;
            deleteNameTextBox.TextChanged += deleteNameTextBox_TextChanged;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(512, 261);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(94, 29);
            deleteButton.TabIndex = 9;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // addLabel
            // 
            addLabel.AutoSize = true;
            addLabel.Location = new Point(250, 122);
            addLabel.Name = "addLabel";
            addLabel.Size = new Size(156, 20);
            addLabel.TabIndex = 10;
            addLabel.Text = "Or add item manually:";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(250, 145);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Name";
            nameTextBox.Size = new Size(150, 27);
            nameTextBox.TabIndex = 11;
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(406, 145);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.PlaceholderText = "Price";
            priceTextBox.Size = new Size(100, 27);
            priceTextBox.TabIndex = 12;
            // 
            // qtyTextBox
            // 
            qtyTextBox.Location = new Point(512, 145);
            qtyTextBox.Name = "qtyTextBox";
            qtyTextBox.PlaceholderText = "Qty";
            qtyTextBox.Size = new Size(60, 27);
            qtyTextBox.TabIndex = 13;
            // 
            // addManualButton
            // 
            addManualButton.Location = new Point(578, 144);
            addManualButton.Name = "addManualButton";
            addManualButton.Size = new Size(94, 29);
            addManualButton.TabIndex = 14;
            addManualButton.Text = "Add";
            addManualButton.UseVisualStyleBackColor = true;
            addManualButton.Click += addManualButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(395, 201);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 15;
            label3.Text = "Item Deletion";
            // 
            // deleteSuggestionsListBox
            // 
            deleteSuggestionsListBox.Location = new Point(306, 292);
            deleteSuggestionsListBox.Name = "deleteSuggestionsListBox";
            deleteSuggestionsListBox.Size = new Size(300, 84);
            deleteSuggestionsListBox.TabIndex = 16;
            deleteSuggestionsListBox.Visible = false;
            deleteSuggestionsListBox.Click += deleteSuggestionsListBox_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(deleteSuggestionsListBox);
            Controls.Add(label3);
            Controls.Add(addManualButton);
            Controls.Add(qtyTextBox);
            Controls.Add(priceTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(addLabel);
            Controls.Add(deleteButton);
            Controls.Add(deleteNameTextBox);
            Controls.Add(deleteLabel);
            Controls.Add(label2);
            Controls.Add(uploadButton);
            Controls.Add(label1);
            Name = "Form3";
            Text = "Manage Items";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button uploadButton;
        private Label label2;
        private Label deleteLabel;
        private TextBox deleteNameTextBox;
        private Button deleteButton;
        private Label addLabel;
        private TextBox nameTextBox;
        private TextBox priceTextBox;
        private TextBox qtyTextBox;
        private Button addManualButton;
        private Label label3;
        private ListBox deleteSuggestionsListBox;
    }
}


