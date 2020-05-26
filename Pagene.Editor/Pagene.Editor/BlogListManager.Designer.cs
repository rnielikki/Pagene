namespace Pagene.Editor
{
    partial class BlogListManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BlogListUI = new System.Windows.Forms.ListBox();
            this.EditButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            // 
            // BlogListUI
            // 
            this.BlogListUI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlogListUI.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BlogListUI.FormattingEnabled = true;
            this.BlogListUI.ItemHeight = 25;
            this.BlogListUI.Location = new System.Drawing.Point(20, 11);
            this.BlogListUI.Margin = new System.Windows.Forms.Padding(13);
            this.BlogListUI.Name = "BlogListUI";
            this.BlogListUI.Size = new System.Drawing.Size(767, 504);
            this.BlogListUI.TabIndex = 0;
            // 
            // EditButton
            // 
            this.EditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EditButton.BackColor = System.Drawing.SystemColors.Info;
            this.EditButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EditButton.ForeColor = System.Drawing.SystemColors.InfoText;
            this.EditButton.Location = new System.Drawing.Point(131, 521);
            this.EditButton.Margin = new System.Windows.Forms.Padding(13);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(105, 37);
            this.EditButton.TabIndex = 1;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = false;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddButton.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.AddButton.Location = new System.Drawing.Point(20, 521);
            this.AddButton.Margin = new System.Windows.Forms.Padding(13);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(105, 37);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ConvertButton
            // 
            this.ConvertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConvertButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConvertButton.Location = new System.Drawing.Point(642, 521);
            this.ConvertButton.Margin = new System.Windows.Forms.Padding(13);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(145, 37);
            this.ConvertButton.TabIndex = 3;
            this.ConvertButton.Text = "Convert";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeleteButton.Location = new System.Drawing.Point(243, 521);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(13);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(107, 37);
            this.DeleteButton.TabIndex = 4;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // BlogListManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 565);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.BlogListUI);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "BlogListManager";
            this.Text = "Main";

        }

        #endregion

        private System.Windows.Forms.ListBox BlogListUI;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.Button DeleteButton;
    }
}