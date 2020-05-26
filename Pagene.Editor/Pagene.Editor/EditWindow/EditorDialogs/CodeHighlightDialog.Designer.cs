namespace Pagene.Editor.EditWindow.EditorDialogs
{
    partial class CodeHighlightDialog
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.LanguageLabel = new System.Windows.Forms.Label();
            // 
            // comboBox1
            // 
            this.comboBox1.AllowDrop = true;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(14, 54);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(209, 33);
            this.comboBox1.TabIndex = 0;
            // 
            // LanguageLabel
            // 
            this.LanguageLabel.AutoSize = true;
            this.LanguageLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LanguageLabel.Location = new System.Drawing.Point(14, 21);
            this.LanguageLabel.Name = "LanguageLabel";
            this.LanguageLabel.Size = new System.Drawing.Size(212, 25);
            this.LanguageLabel.TabIndex = 1;
            this.LanguageLabel.Text = "Programming language";
            // 
            // CodeHighlightDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 162);
            this.Controls.Add(this.LanguageLabel);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CodeHighlightDialog";
            this.Text = "CodeHighlightDialog";

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label LanguageLabel;
    }
}