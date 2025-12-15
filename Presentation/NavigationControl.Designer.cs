namespace Inventory_Management.Forms
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
            manageItemsButton = new Button();
            manageInventoryButton = new Button();
            overviewButton = new Button();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox
            // 
            groupBox.Controls.Add(manageItemsButton);
            groupBox.Controls.Add(manageInventoryButton);
            groupBox.Controls.Add(overviewButton);
            groupBox.Location = new Point(0, 0);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(139, 452);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            groupBox.Text = "Navigation";
            groupBox.ForeColor = Color.White;
            // 
            // manageItemsButton
            // 
            manageItemsButton.BackColor = Color.FromArgb(50, 50, 50);
            manageItemsButton.FlatStyle = FlatStyle.Flat;
            manageItemsButton.FlatAppearance.BorderSize = 0;
            manageItemsButton.ForeColor = Color.White;
            manageItemsButton.Location = new Point(7, 96);
            manageItemsButton.Name = "manageItemsButton";
            manageItemsButton.Size = new Size(125, 29);
            manageItemsButton.TabIndex = 4;
            manageItemsButton.Text = "Manage Items";
            manageItemsButton.UseVisualStyleBackColor = false;
            manageItemsButton.Click += manageItemsButton_Click;
            // 
            // manageInventoryButton
            // 
            manageInventoryButton.BackColor = Color.FromArgb(50, 50, 50);
            manageInventoryButton.FlatStyle = FlatStyle.Flat;
            manageInventoryButton.FlatAppearance.BorderSize = 0;
            manageInventoryButton.ForeColor = Color.White;
            manageInventoryButton.Location = new Point(7, 61);
            manageInventoryButton.Name = "manageInventoryButton";
            manageInventoryButton.Size = new Size(125, 29);
            manageInventoryButton.TabIndex = 2;
            manageInventoryButton.Text = "Inventory";
            manageInventoryButton.UseVisualStyleBackColor = false;
            manageInventoryButton.Click += manageInventoryButton_Click;
            // 
            // overviewButton
            // 
            overviewButton.BackColor = Color.FromArgb(50, 50, 50);
            overviewButton.FlatStyle = FlatStyle.Flat;
            overviewButton.FlatAppearance.BorderSize = 0;
            overviewButton.ForeColor = Color.White;
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
            BackColor = Color.FromArgb(34, 34, 34);
            Controls.Add(groupBox);
            Name = "NavigationControl";
            Size = new Size(139, 452);
            groupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox;
        private Button overviewButton;
        private Button manageInventoryButton;
        private Button manageItemsButton;
    }
}
