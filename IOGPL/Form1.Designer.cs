﻿namespace IOGPL
{
    partial class Form1
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
            this.runBtn = new System.Windows.Forms.Button();
            this.syntaxBtn = new System.Windows.Forms.Button();
            this.pBox = new System.Windows.Forms.PictureBox();
            this.rTextBox = new System.Windows.Forms.RichTextBox();
            this.txtBox = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            this.SuspendLayout();
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(268, 406);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(75, 36);
            this.runBtn.TabIndex = 0;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            // 
            // syntaxBtn
            // 
            this.syntaxBtn.Location = new System.Drawing.Point(349, 406);
            this.syntaxBtn.Name = "syntaxBtn";
            this.syntaxBtn.Size = new System.Drawing.Size(75, 36);
            this.syntaxBtn.TabIndex = 1;
            this.syntaxBtn.Text = "Syntax";
            this.syntaxBtn.UseVisualStyleBackColor = true;
            // 
            // pBox
            // 
            this.pBox.Location = new System.Drawing.Point(454, 40);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(416, 389);
            this.pBox.TabIndex = 2;
            this.pBox.TabStop = false;
            // 
            // rTextBox
            // 
            this.rTextBox.Location = new System.Drawing.Point(12, 66);
            this.rTextBox.Name = "rTextBox";
            this.rTextBox.Size = new System.Drawing.Size(412, 302);
            this.rTextBox.TabIndex = 3;
            this.rTextBox.Text = "";
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(12, 374);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(412, 26);
            this.txtBox.TabIndex = 4;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(12, 26);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 34);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(93, 26);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(75, 34);
            this.loadBtn.TabIndex = 6;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 539);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.rTextBox);
            this.Controls.Add(this.pBox);
            this.Controls.Add(this.syntaxBtn);
            this.Controls.Add(this.runBtn);
            this.Name = "Form1";
            this.Text = "IO Graphical Language";
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button syntaxBtn;
        private System.Windows.Forms.PictureBox pBox;
        private System.Windows.Forms.RichTextBox rTextBox;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button loadBtn;
    }
}