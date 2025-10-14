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
            this.groupBox = new GroupBox();
            this.checkoutButton = new Button();
            this.projectionsButton = new Button();
            this.editItemButton = new Button();
            this.manageItemsButton = new Button();
            this.addStockButton = new Button();
            this.viewInventoryButton = new Button();
            this.overviewButton = new Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.checkoutButton);
            this.groupBox.Controls.Add(this.projectionsButton);
            this.groupBox.Controls.Add(this.editItemButton);
            this.groupBox.Controls.Add(this.manageItemsButton);
            this.groupBox.Controls.Add(this.addStockButton);
            this.groupBox.Controls.Add(this.viewInventoryButton);
            this.groupBox.Controls.Add(this.overviewButton);
            this.groupBox.Location = new Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new Size(139, 452);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Navigation";
            // 
            // checkoutButton
            // 
            this.checkoutButton.BackColor = SystemColors.ControlDarkDark;
            this.checkoutButton.Location = new Point(7, 236);
            this.checkoutButton.Name = "checkoutButton";
            this.checkoutButton.Size = new Size(125, 29);
            this.checkoutButton.TabIndex = 7;
            this.checkoutButton.Text = "Checkout";
            this.checkoutButton.UseVisualStyleBackColor = false;
            this.checkoutButton.Click += checkoutButton_Click;
            // 
            // projectionsButton
            // 
            this.projectionsButton.BackColor = SystemColors.ControlDarkDark;
            this.projectionsButton.Location = new Point(7, 201);
            this.projectionsButton.Name = "projectionsButton";
            this.projectionsButton.Size = new Size(125, 29);
            this.projectionsButton.TabIndex = 6;
            this.projectionsButton.Text = "Projections";
            this.projectionsButton.UseVisualStyleBackColor = false;
            this.projectionsButton.Click += projectionsButton_Click;
            // 
            // editItemButton
            // 
            this.editItemButton.BackColor = SystemColors.ControlDarkDark;
            this.editItemButton.Location = new Point(7, 166);
            this.editItemButton.Name = "editItemButton";
            this.editItemButton.Size = new Size(125, 29);
            this.editItemButton.TabIndex = 5;
            this.editItemButton.Text = "Edit Item";
            this.editItemButton.UseVisualStyleBackColor = false;
            // 
            // manageItemsButton
            // 
            this.manageItemsButton.BackColor = SystemColors.Control;
            this.manageItemsButton.Location = new Point(7, 131);
            this.manageItemsButton.Name = "manageItemsButton";
            this.manageItemsButton.Size = new Size(125, 29);
            this.manageItemsButton.TabIndex = 4;
            this.manageItemsButton.Text = "Manage Items";
            this.manageItemsButton.UseVisualStyleBackColor = true;
            this.manageItemsButton.Click += manageItemsButton_Click;
            // 
            // addStockButton
            // 
            this.addStockButton.BackColor = SystemColors.ControlDarkDark;
            this.addStockButton.Location = new Point(7, 96);
            this.addStockButton.Name = "addStockButton";
            this.addStockButton.RightToLeft = RightToLeft.Yes;
            this.addStockButton.Size = new Size(125, 29);
            this.addStockButton.TabIndex = 3;
            this.addStockButton.Text = "Add Stock";
            this.addStockButton.UseVisualStyleBackColor = false;
            this.addStockButton.Click += addStockButton_Click;
            // 
            // viewInventoryButton
            // 
            this.viewInventoryButton.Location = new Point(7, 61);
            this.viewInventoryButton.Name = "viewInventoryButton";
            this.viewInventoryButton.Size = new Size(125, 29);
            this.viewInventoryButton.TabIndex = 2;
            this.viewInventoryButton.Text = "View Inventory";
            this.viewInventoryButton.UseVisualStyleBackColor = true;
            this.viewInventoryButton.Click += viewInventoryButton_Click;
            // 
            // overviewButton
            // 
            this.overviewButton.BackColor = SystemColors.ActiveCaption;
            this.overviewButton.FlatStyle = FlatStyle.Popup;
            this.overviewButton.ForeColor = SystemColors.ActiveCaptionText;
            this.overviewButton.Location = new Point(7, 26);
            this.overviewButton.Name = "overviewButton";
            this.overviewButton.Size = new Size(125, 29);
            this.overviewButton.TabIndex = 1;
            this.overviewButton.Text = "Overview";
            this.overviewButton.UseVisualStyleBackColor = false;
            this.overviewButton.Click += overviewButton_Click;
            // 
            // NavigationControl
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "NavigationControl";
            this.Size = new Size(139, 452);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);
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


