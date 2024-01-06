﻿namespace TranslationTool
{
    partial class MainForm
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
            this.FilePathButton = new System.Windows.Forms.Button();
            this.FilePathLabel = new System.Windows.Forms.Label();
            this.FormatButton = new System.Windows.Forms.Button();
            this.ExtractButton = new System.Windows.Forms.Button();
            this.InsertButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilePathButton
            // 
            this.FilePathButton.Location = new System.Drawing.Point(12, 61);
            this.FilePathButton.Name = "FilePathButton";
            this.FilePathButton.Size = new System.Drawing.Size(128, 32);
            this.FilePathButton.TabIndex = 0;
            this.FilePathButton.Text = "Open";
            this.FilePathButton.UseVisualStyleBackColor = true;
            this.FilePathButton.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // FilePathLabel
            // 
            this.FilePathLabel.Location = new System.Drawing.Point(12, 8);
            this.FilePathLabel.Name = "FilePathLabel";
            this.FilePathLabel.Size = new System.Drawing.Size(616, 38);
            this.FilePathLabel.TabIndex = 1;
            this.FilePathLabel.Text = "...";
            // 
            // FormatButton
            // 
            this.FormatButton.Location = new System.Drawing.Point(146, 61);
            this.FormatButton.Name = "FormatButton";
            this.FormatButton.Size = new System.Drawing.Size(128, 32);
            this.FormatButton.TabIndex = 2;
            this.FormatButton.Text = "Format";
            this.FormatButton.UseVisualStyleBackColor = true;
            this.FormatButton.Click += new System.EventHandler(this.OnFormatClick);
            // 
            // ExtractButton
            // 
            this.ExtractButton.Location = new System.Drawing.Point(280, 61);
            this.ExtractButton.Name = "ExtractButton";
            this.ExtractButton.Size = new System.Drawing.Size(128, 32);
            this.ExtractButton.TabIndex = 3;
            this.ExtractButton.Text = "Extract";
            this.ExtractButton.UseVisualStyleBackColor = true;
            this.ExtractButton.Click += new System.EventHandler(this.OnExtractClick);
            // 
            // InsertButton
            // 
            this.InsertButton.Location = new System.Drawing.Point(414, 61);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(128, 32);
            this.InsertButton.TabIndex = 4;
            this.InsertButton.Text = "Insert";
            this.InsertButton.UseVisualStyleBackColor = true;
            this.InsertButton.Click += new System.EventHandler(this.OnInsertClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 861);
            this.Controls.Add(this.InsertButton);
            this.Controls.Add(this.ExtractButton);
            this.Controls.Add(this.FormatButton);
            this.Controls.Add(this.FilePathLabel);
            this.Controls.Add(this.FilePathButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Translation Tool";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button InsertButton;

        private System.Windows.Forms.Button ExtractButton;

        private System.Windows.Forms.Button FormatButton;

        private System.Windows.Forms.Label FilePathLabel;

        private System.Windows.Forms.Button FilePathButton;

        #endregion
    }
}