namespace Pagene.Editor
{
    partial class LinkWindow
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
            this.LinkTarget = new System.Windows.Forms.TextBox();
            this.TitleTarget = new System.Windows.Forms.TextBox();
            this.LinkTargetLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            // 
            // LinkTarget
            // 
            this.LinkTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkTarget.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LinkTarget.Location = new System.Drawing.Point(20, 43);
            this.LinkTarget.Name = "LinkTarget";
            this.LinkTarget.Size = new System.Drawing.Size(723, 33);
            this.LinkTarget.TabIndex = 0;
            // 
            // TitleTarget
            // 
            this.TitleTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleTarget.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleTarget.Location = new System.Drawing.Point(20, 124);
            this.TitleTarget.Name = "TitleTarget";
            this.TitleTarget.Size = new System.Drawing.Size(723, 33);
            this.TitleTarget.TabIndex = 0;
            // 
            // LinkTargetLabel
            // 
            this.LinkTargetLabel.AutoSize = true;
            this.LinkTargetLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LinkTargetLabel.Location = new System.Drawing.Point(20, 15);
            this.LinkTargetLabel.Name = "LinkTargetLabel";
            this.LinkTargetLabel.Size = new System.Drawing.Size(39, 21);
            this.LinkTargetLabel.TabIndex = 1;
            this.LinkTargetLabel.Text = "URL";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.Location = new System.Drawing.Point(20, 95);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(113, 21);
            this.TitleLabel.TabIndex = 2;
            this.TitleLabel.Text = "Title (Optional)";
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OKButton.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.OKButton.Location = new System.Drawing.Point(20, 176);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 36);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloseButton.Location = new System.Drawing.Point(113, 176);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(89, 36);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // LinkWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 228);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.LinkTargetLabel);
            this.Controls.Add(this.LinkTarget);
            this.Controls.Add(this.TitleTarget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "LinkWindow";
            this.Text = "Add Link";
            this.Load += new System.EventHandler(this.LinkWindow_Load);

        }

        #endregion

        private System.Windows.Forms.TextBox LinkTarget;
        private System.Windows.Forms.TextBox TitleTarget;
        private System.Windows.Forms.Label LinkTargetLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CloseButton;
    }
}