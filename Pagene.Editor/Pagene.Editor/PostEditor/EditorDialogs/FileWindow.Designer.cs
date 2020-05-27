namespace Pagene.Editor
{
    partial class FileWindow
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
            this.FileList = new System.Windows.Forms.ListView();
            this.OpenPathButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PreviewPicture = new System.Windows.Forms.PictureBox();
            this.ErrorMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPicture)).BeginInit();
            // 
            // FileList
            // 
            this.FileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileList.HideSelection = false;
            this.FileList.Location = new System.Drawing.Point(24, 24);
            this.FileList.MultiSelect = false;
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(459, 340);
            this.FileList.TabIndex = 0;
            this.FileList.UseCompatibleStateImageBehavior = false;
            // 
            // OpenPathButton
            // 
            this.OpenPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenPathButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.OpenPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenPathButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenPathButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.OpenPathButton.Location = new System.Drawing.Point(370, 370);
            this.OpenPathButton.Name = "OpenPathButton";
            this.OpenPathButton.Size = new System.Drawing.Size(113, 30);
            this.OpenPathButton.TabIndex = 1;
            this.OpenPathButton.Text = "Open Path...";
            this.OpenPathButton.UseVisualStyleBackColor = false;
            this.OpenPathButton.Click += new System.EventHandler(this.OpenPathButton_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SelectButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.SelectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SelectButton.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.SelectButton.Location = new System.Drawing.Point(24, 399);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(89, 35);
            this.SelectButton.TabIndex = 2;
            this.SelectButton.Text = "Select";
            this.SelectButton.UseVisualStyleBackColor = false;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.BackColor = System.Drawing.Color.Pink;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloseButton.ForeColor = System.Drawing.Color.Maroon;
            this.CloseButton.Location = new System.Drawing.Point(132, 399);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(85, 35);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PreviewPicture
            // 
            this.PreviewPicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewPicture.Location = new System.Drawing.Point(500, 24);
            this.PreviewPicture.Name = "PreviewPicture";
            this.PreviewPicture.Size = new System.Drawing.Size(307, 340);
            this.PreviewPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PreviewPicture.TabIndex = 4;
            this.PreviewPicture.TabStop = false;
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorMessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ErrorMessage.ForeColor = System.Drawing.Color.Crimson;
            this.ErrorMessage.Location = new System.Drawing.Point(500, 167);
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Size = new System.Drawing.Size(307, 56);
            this.ErrorMessage.TabIndex = 5;
            this.ErrorMessage.Text = "Error: The file is corrupted";
            this.ErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 450);
            this.Controls.Add(this.ErrorMessage);
            this.Controls.Add(this.PreviewPicture);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.OpenPathButton);
            this.Controls.Add(this.FileList);
            this.Name = "FileWindow";
            this.Text = "Select a file";
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPicture)).EndInit();

        }

        #endregion

        private System.Windows.Forms.ListView FileList;
        private System.Windows.Forms.Button OpenPathButton;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox PreviewPicture;
        private System.Windows.Forms.Label ErrorMessage;
    }
}