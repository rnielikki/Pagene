using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Pagene.Editor
{
    partial class PreviewWindow
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.HtmlTagBox = new System.Windows.Forms.TextBox();
            this.TitleBox = new System.Windows.Forms.Label();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloseButton.Location = new System.Drawing.Point(22, 401);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(85, 41);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // HtmlTagBox
            // 
            this.HtmlTagBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HtmlTagBox.Location = new System.Drawing.Point(22, 62);
            this.HtmlTagBox.Multiline = true;
            this.HtmlTagBox.Name = "HtmlTagBox";
            this.HtmlTagBox.Size = new System.Drawing.Size(764, 333);
            this.HtmlTagBox.TabIndex = 2;
            // 
            // TitleBox
            // 
            this.TitleBox.AutoSize = true;
            this.TitleBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleBox.Location = new System.Drawing.Point(30, 15);
            this.TitleBox.Name = "TitleBox";
            this.TitleBox.Size = new System.Drawing.Size(0, 30);
            this.TitleBox.TabIndex = 3;
            // 
            // PreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TitleBox);
            this.Controls.Add(this.HtmlTagBox);
            this.Controls.Add(this.CloseButton);
            this.Name = "PreviewWindow";
            this.Text = "PreviewWindow";

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private TextBox HtmlTagBox;
        private Label TitleBox;
    }
}