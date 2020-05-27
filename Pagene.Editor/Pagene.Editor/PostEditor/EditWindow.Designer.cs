namespace Pagene.Editor
{
    partial class EditWindow
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
            this.TitleBox = new System.Windows.Forms.TextBox();
            this.ContentBox = new System.Windows.Forms.RichTextBox();
            this.TagBox = new System.Windows.Forms.TextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.AddTagButton = new System.Windows.Forms.Button();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.TagNotation = new System.Windows.Forms.Label();
            this.SyntaxHeadline = new System.Windows.Forms.Button();
            this.SyntaxHeadLine2 = new System.Windows.Forms.Button();
            this.SyntaxBoldText = new System.Windows.Forms.Button();
            this.SyntaxLink = new System.Windows.Forms.Button();
            this.SyntaxItalic = new System.Windows.Forms.Button();
            this.PreviewButton = new System.Windows.Forms.Button();
            this.SyntaxImage = new System.Windows.Forms.Button();
            this.SyntaxHorizon = new System.Windows.Forms.Button();
            this.SyntaxQuote = new System.Windows.Forms.Button();
            this.SyntaxCodeInline = new System.Windows.Forms.Button();
            this.SyntaxCode = new System.Windows.Forms.Button();
            // 
            // TitleBox
            // 
            this.TitleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleBox.Location = new System.Drawing.Point(67, 25);
            this.TitleBox.Name = "TitleBox";
            this.TitleBox.Size = new System.Drawing.Size(712, 33);
            this.TitleBox.TabIndex = 0;
            // 
            // ContentBox
            // 
            this.ContentBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ContentBox.Location = new System.Drawing.Point(13, 113);
            this.ContentBox.Name = "ContentBox";
            this.ContentBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ContentBox.Size = new System.Drawing.Size(766, 305);
            this.ContentBox.TabIndex = 1;
            this.ContentBox.Text = "";
            // 
            // TagBox
            // 
            this.TagBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TagBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TagBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TagBox.Location = new System.Drawing.Point(576, 458);
            this.TagBox.Name = "TagBox";
            this.TagBox.Size = new System.Drawing.Size(159, 33);
            this.TagBox.TabIndex = 2;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(0, 0);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(100, 23);
            this.maskedTextBox1.TabIndex = 3;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.SaveButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveButton.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.SaveButton.Location = new System.Drawing.Point(133, 490);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(121, 35);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.BackColor = System.Drawing.Color.MistyRose;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloseButton.ForeColor = System.Drawing.Color.DarkRed;
            this.CloseButton.Location = new System.Drawing.Point(260, 490);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(121, 35);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // AddTagButton
            // 
            this.AddTagButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddTagButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddTagButton.Location = new System.Drawing.Point(741, 458);
            this.AddTagButton.Name = "AddTagButton";
            this.AddTagButton.Size = new System.Drawing.Size(33, 33);
            this.AddTagButton.TabIndex = 5;
            this.AddTagButton.Text = "+";
            this.AddTagButton.UseVisualStyleBackColor = true;
            this.AddTagButton.Click += new System.EventHandler(this.AddTagButton_Click);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(0, 0);
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(100, 23);
            this.maskedTextBox2.TabIndex = 6;
            this.maskedTextBox2.Text = "maskedTextBox2";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.Location = new System.Drawing.Point(13, 25);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(48, 25);
            this.TitleLabel.TabIndex = 6;
            this.TitleLabel.Text = "Title";
            // 
            // TagNotation
            // 
            this.TagNotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TagNotation.AutoSize = true;
            this.TagNotation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TagNotation.Location = new System.Drawing.Point(26, 425);
            this.TagNotation.Name = "TagNotation";
            this.TagNotation.Size = new System.Drawing.Size(193, 21);
            this.TagNotation.TabIndex = 7;
            this.TagNotation.Text = "Tags: Click to remove a tag";
            // 
            // SyntaxHeadline
            // 
            this.SyntaxHeadline.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SyntaxHeadline.Location = new System.Drawing.Point(13, 71);
            this.SyntaxHeadline.Name = "SyntaxHeadline";
            this.SyntaxHeadline.Size = new System.Drawing.Size(41, 36);
            this.SyntaxHeadline.TabIndex = 8;
            this.SyntaxHeadline.Text = "H1";
            this.SyntaxHeadline.UseVisualStyleBackColor = true;
            this.SyntaxHeadline.Click += new System.EventHandler(this.SyntaxHeadline_Click);
            // 
            // SyntaxHeadLine2
            // 
            this.SyntaxHeadLine2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SyntaxHeadLine2.Location = new System.Drawing.Point(60, 71);
            this.SyntaxHeadLine2.Name = "SyntaxHeadLine2";
            this.SyntaxHeadLine2.Size = new System.Drawing.Size(41, 36);
            this.SyntaxHeadLine2.TabIndex = 8;
            this.SyntaxHeadLine2.Text = "H2";
            this.SyntaxHeadLine2.UseVisualStyleBackColor = true;
            this.SyntaxHeadLine2.Click += new System.EventHandler(this.SyntaxHeadline2_Click);
            // 
            // SyntaxBoldText
            // 
            this.SyntaxBoldText.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SyntaxBoldText.Location = new System.Drawing.Point(109, 71);
            this.SyntaxBoldText.Name = "SyntaxBoldText";
            this.SyntaxBoldText.Size = new System.Drawing.Size(35, 36);
            this.SyntaxBoldText.TabIndex = 9;
            this.SyntaxBoldText.Text = "B";
            this.SyntaxBoldText.UseVisualStyleBackColor = true;
            this.SyntaxBoldText.Click += new System.EventHandler(this.SyntaxBoldText_Click);
            // 
            // SyntaxLink
            // 
            this.SyntaxLink.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SyntaxLink.Location = new System.Drawing.Point(193, 71);
            this.SyntaxLink.Name = "SyntaxLink";
            this.SyntaxLink.Size = new System.Drawing.Size(68, 36);
            this.SyntaxLink.TabIndex = 10;
            this.SyntaxLink.Text = "Link";
            this.SyntaxLink.UseVisualStyleBackColor = true;
            this.SyntaxLink.Click += new System.EventHandler(this.SyntaxLink_Click);
            // 
            // SyntaxItalic
            // 
            this.SyntaxItalic.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.SyntaxItalic.Location = new System.Drawing.Point(150, 71);
            this.SyntaxItalic.Name = "SyntaxItalic";
            this.SyntaxItalic.Size = new System.Drawing.Size(37, 36);
            this.SyntaxItalic.TabIndex = 11;
            this.SyntaxItalic.Text = "I";
            this.SyntaxItalic.UseVisualStyleBackColor = true;
            this.SyntaxItalic.Click += new System.EventHandler(this.SyntaxItalic_Click);
            // 
            // PreviewButton
            // 
            this.PreviewButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.PreviewButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.PreviewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviewButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PreviewButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PreviewButton.Location = new System.Drawing.Point(13, 490);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.Size = new System.Drawing.Size(114, 35);
            this.PreviewButton.TabIndex = 13;
            this.PreviewButton.Text = "Preview";
            this.PreviewButton.UseVisualStyleBackColor = false;
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // SyntaxImage
            // 
            this.SyntaxImage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SyntaxImage.Location = new System.Drawing.Point(267, 71);
            this.SyntaxImage.Name = "SyntaxImage";
            this.SyntaxImage.Size = new System.Drawing.Size(75, 36);
            this.SyntaxImage.TabIndex = 14;
            this.SyntaxImage.Text = "Image";
            this.SyntaxImage.UseVisualStyleBackColor = true;
            this.SyntaxImage.Click += new System.EventHandler(SyntaxImage_Click);
            // 
            // SyntaxHorizon
            // 
            this.SyntaxHorizon.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SyntaxHorizon.Location = new System.Drawing.Point(348, 71);
            this.SyntaxHorizon.Name = "SyntaxHorizon";
            this.SyntaxHorizon.Size = new System.Drawing.Size(47, 36);
            this.SyntaxHorizon.TabIndex = 15;
            this.SyntaxHorizon.Text = "---";
            this.SyntaxHorizon.UseVisualStyleBackColor = true;
            this.SyntaxHorizon.Click += new System.EventHandler(this.SyntaxHorizon_Click);
            // 
            // SyntaxQuote
            // 
            this.SyntaxQuote.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SyntaxQuote.Location = new System.Drawing.Point(401, 71);
            this.SyntaxQuote.Name = "SyntaxQuote";
            this.SyntaxQuote.Size = new System.Drawing.Size(40, 36);
            this.SyntaxQuote.TabIndex = 16;
            this.SyntaxQuote.Text = "“ ”";
            this.SyntaxQuote.UseVisualStyleBackColor = true;
            this.SyntaxQuote.Click += new System.EventHandler(this.SyntaxQuote_Click);
            // 
            // SyntaxCodeInline
            // 
            this.SyntaxCodeInline.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SyntaxCodeInline.Location = new System.Drawing.Point(447, 71);
            this.SyntaxCodeInline.Name = "SyntaxCodeInline";
            this.SyntaxCodeInline.Size = new System.Drawing.Size(59, 36);
            this.SyntaxCodeInline.TabIndex = 17;
            this.SyntaxCodeInline.Text = "< />";
            this.SyntaxCodeInline.UseVisualStyleBackColor = true;
            this.SyntaxCodeInline.Click += new System.EventHandler(this.SyntaxCodeInline_Click);
            // 
            // SyntaxCode
            // 
            this.SyntaxCode.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SyntaxCode.Location = new System.Drawing.Point(512, 71);
            this.SyntaxCode.Name = "SyntaxCode";
            this.SyntaxCode.Size = new System.Drawing.Size(45, 36);
            this.SyntaxCode.TabIndex = 18;
            this.SyntaxCode.Text = "{ }";
            this.SyntaxCode.UseVisualStyleBackColor = true;
            this.SyntaxCode.Click += new System.EventHandler(this.SyntaxCode_Click);
            // 
            // EditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 541);
            this.Controls.Add(this.SyntaxCode);
            this.Controls.Add(this.SyntaxCodeInline);
            this.Controls.Add(this.SyntaxQuote);
            this.Controls.Add(this.SyntaxHorizon);
            this.Controls.Add(this.SyntaxImage);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.SyntaxItalic);
            this.Controls.Add(this.SyntaxLink);
            this.Controls.Add(this.SyntaxBoldText);
            this.Controls.Add(this.SyntaxHeadLine2);
            this.Controls.Add(this.SyntaxHeadline);
            this.Controls.Add(this.TagNotation);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.AddTagButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TagBox);
            this.Controls.Add(this.ContentBox);
            this.Controls.Add(this.TitleBox);
            this.Name = "EditWindow";
            this.Text = "EditWindow";

        }

        #endregion

        private System.Windows.Forms.TextBox TitleBox;
        private System.Windows.Forms.RichTextBox ContentBox;
        private System.Windows.Forms.TextBox TagBox;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button AddTagButton;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label TagNotation;
        private System.Windows.Forms.Button SyntaxHeadline;
        private System.Windows.Forms.Button SyntaxHeadLine2;
        private System.Windows.Forms.Button SyntaxBoldText;
        private System.Windows.Forms.Button SyntaxLink;
        private System.Windows.Forms.Button SyntaxItalic;
        private System.Windows.Forms.Button PreviewButton;
        private System.Windows.Forms.Button SyntaxImage;
        private System.Windows.Forms.Button SyntaxHorizon;
        private System.Windows.Forms.Button SyntaxQuote;
        private System.Windows.Forms.Button SyntaxCodeInline;
        private System.Windows.Forms.Button SyntaxCode;
    }
}