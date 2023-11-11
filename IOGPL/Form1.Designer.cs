namespace IOGPL
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
            this.cmdTxtBox = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            this.SuspendLayout();
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(179, 264);
            this.runBtn.Margin = new System.Windows.Forms.Padding(2);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(50, 23);
            this.runBtn.TabIndex = 0;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // syntaxBtn
            // 
            this.syntaxBtn.Location = new System.Drawing.Point(233, 264);
            this.syntaxBtn.Margin = new System.Windows.Forms.Padding(2);
            this.syntaxBtn.Name = "syntaxBtn";
            this.syntaxBtn.Size = new System.Drawing.Size(50, 23);
            this.syntaxBtn.TabIndex = 1;
            this.syntaxBtn.Text = "Syntax";
            this.syntaxBtn.UseVisualStyleBackColor = true;
            // 
            // pBox
            // 
            this.pBox.BackColor = System.Drawing.Color.LightGray;
            this.pBox.Location = new System.Drawing.Point(303, 26);
            this.pBox.Margin = new System.Windows.Forms.Padding(2);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(277, 253);
            this.pBox.TabIndex = 2;
            this.pBox.TabStop = false;
            this.pBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pBox_Paint);
            // 
            // rTextBox
            // 
            this.rTextBox.Location = new System.Drawing.Point(8, 43);
            this.rTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.rTextBox.Name = "rTextBox";
            this.rTextBox.Size = new System.Drawing.Size(276, 198);
            this.rTextBox.TabIndex = 3;
            this.rTextBox.Text = "";
            // 
            // cmdTxtBox
            // 
            this.cmdTxtBox.Location = new System.Drawing.Point(8, 243);
            this.cmdTxtBox.Margin = new System.Windows.Forms.Padding(2);
            this.cmdTxtBox.Name = "cmdTxtBox";
            this.cmdTxtBox.Size = new System.Drawing.Size(276, 20);
            this.cmdTxtBox.TabIndex = 4;
            this.cmdTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmdTxtBox_KeyPress);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(8, 17);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(2);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(50, 22);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(62, 17);
            this.loadBtn.Margin = new System.Windows.Forms.Padding(2);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(50, 22);
            this.loadBtn.TabIndex = 6;
            this.loadBtn.Text = "Open";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 350);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.cmdTxtBox);
            this.Controls.Add(this.rTextBox);
            this.Controls.Add(this.pBox);
            this.Controls.Add(this.syntaxBtn);
            this.Controls.Add(this.runBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "IO Graphical Language";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button syntaxBtn;
        private System.Windows.Forms.PictureBox pBox;
        private System.Windows.Forms.RichTextBox rTextBox;
        private System.Windows.Forms.TextBox cmdTxtBox;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button loadBtn;
    }
}