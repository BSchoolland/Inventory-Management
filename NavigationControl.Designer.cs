namespace Inventory_Management
{
    partial class NavigationControl
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

        private void InitializeComponent()
        {
            groupBox = new GroupBox();
            checkoutButton = new Button();
            projectionsButton = new Button();
            editItemButton = new Button();
            manageItemsButton = new Button();
            addStockButton = new Button();
            viewInventoryButton = new Button();
            overviewButton = new Button();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox
            // 
            groupBox.Controls.Add(checkoutButton);
            groupBox.Controls.Add(projectionsButton);
            groupBox.Controls.Add(editItemButton);
            groupBox.Controls.Add(manageItemsButton);
            groupBox.Controls.Add(addStockButton);
            groupBox.Controls.Add(viewInventoryButton);
            groupBox.Controls.Add(overviewButton);
            groupBox.Location = new Point(0, 0);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(139, 452);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            groupBox.Text = "Navigation";
            // 
            // checkoutButton
            // 
            checkoutButton.BackColor = SystemColors.ControlDarkDark;
            checkoutButton.Location = new Point(7, 236);
            checkoutButton.Name = "checkoutButton";
            checkoutButton.Size = new Size(125, 29);
            checkoutButton.TabIndex = 7;
            checkoutButton.Text = "Checkout";
            checkoutButton.UseVisualStyleBackColor = false;
            checkoutButton.Click += checkoutButton_Click;
            // 
            // projectionsButton
            // 
            projectionsButton.BackColor = SystemColors.ControlDarkDark;
            projectionsButton.Location = new Point(7, 201);
            projectionsButton.Name = "projectionsButton";
            projectionsButton.Size = new Size(125, 29);
            projectionsButton.TabIndex = 6;
            projectionsButton.Text = "Projections";
            projectionsButton.UseVisualStyleBackColor = false;
            projectionsButton.Click += projectionsButton_Click;
            // 
            // editItemButton
            // 
            editItemButton.BackColor = SystemColors.ControlDarkDark;
            editItemButton.Location = new Point(7, 166);
            editItemButton.Name = "editItemButton";
            editItemButton.Size = new Size(125, 29);
            editItemButton.TabIndex = 5;
            editItemButton.Text = "Edit Item";
            editItemButton.UseVisualStyleBackColor = false;
            // 
            // manageItemsButton
            // 
            manageItemsButton.BackColor = SystemColors.Control;
            manageItemsButton.Location = new Point(7, 131);
            manageItemsButton.Name = "manageItemsButton";
            manageItemsButton.Size = new Size(125, 29);
            manageItemsButton.TabIndex = 4;
            manageItemsButton.Text = "Manage Items";
            manageItemsButton.UseVisualStyleBackColor = true;
            manageItemsButton.Click += manageItemsButton_Click;
            // 
            // addStockButton
            // 
            addStockButton.BackColor = SystemColors.Control;
            addStockButton.Location = new Point(7, 96);
            addStockButton.Name = "addStockButton";
            addStockButton.RightToLeft = RightToLeft.Yes;
            addStockButton.Size = new Size(125, 29);
            addStockButton.TabIndex = 3;
            addStockButton.Text = "Add Stock";
            addStockButton.UseVisualStyleBackColor = false;
            addStockButton.Click += addStockButton_Click;
            // 
            // viewInventoryButton
            // 
            viewInventoryButton.Location = new Point(7, 61);
            viewInventoryButton.Name = "viewInventoryButton";
            viewInventoryButton.Size = new Size(125, 29);
            viewInventoryButton.TabIndex = 2;
            viewInventoryButton.Text = "View Inventory";
            viewInventoryButton.UseVisualStyleBackColor = true;
            viewInventoryButton.Click += viewInventoryButton_Click;
            // 
            // overviewButton
            // 
            overviewButton.BackColor = SystemColors.ActiveCaption;
            overviewButton.FlatStyle = FlatStyle.Popup;
            overviewButton.ForeColor = SystemColors.ActiveCaptionText;
            overviewButton.Location = new Point(7, 26);
            overviewButton.Name = "overviewButton";
            overviewButton.Size = new Size(125, 29);
            overviewButton.TabIndex = 1;
            overviewButton.Text = "Overview";
            overviewButton.UseVisualStyleBackColor = false;
            overviewButton.Click += overviewButton_Click;
            // 
            // NavigationControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox);
            Name = "NavigationControl";
            Size = new Size(139, 452);
            groupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox;
        private Button overviewButton;
        private Button viewInventoryButton;
        private Button addStockButton;
        private Button manageItemsButton;
        private Button editItemButton;
        private Button projectionsButton;
        private Button checkoutButton;
    }
}


