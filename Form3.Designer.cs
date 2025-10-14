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
            uploadButton.Location = new Point(229, 120);
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
            label2.Location = new Point(249, 96);
            label2.Name = "label2";
            label2.Size = new Size(99, 20);
            label2.TabIndex = 6;
            label2.Text = "Item uploads:";
            label2.Click += label2_Click_1;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(uploadButton);
            Controls.Add(label1);
            Name = "Form3";
            Text = "Add Items";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button uploadButton;
        private Label label2;
    }
}


