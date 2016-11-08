namespace TitleID_Changer
{
    partial class Main
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
            this.openXex = new System.Windows.Forms.Button();
            this.addID = new System.Windows.Forms.Button();
            this.changeID = new System.Windows.Forms.Button();
            this.mediaID = new System.Windows.Forms.TextBox();
            this.titleID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // openXex
            // 
            this.openXex.Location = new System.Drawing.Point(177, 12);
            this.openXex.Name = "openXex";
            this.openXex.Size = new System.Drawing.Size(91, 23);
            this.openXex.TabIndex = 0;
            this.openXex.Text = "Open XEX";
            this.openXex.UseVisualStyleBackColor = true;
            this.openXex.Click += new System.EventHandler(this.openXex_Click);
            // 
            // addID
            // 
            this.addID.Enabled = false;
            this.addID.Location = new System.Drawing.Point(177, 42);
            this.addID.Name = "addID";
            this.addID.Size = new System.Drawing.Size(91, 23);
            this.addID.TabIndex = 1;
            this.addID.Text = "Add TitleID";
            this.addID.UseVisualStyleBackColor = true;
            this.addID.Click += new System.EventHandler(this.addID_Click);
            // 
            // changeID
            // 
            this.changeID.Enabled = false;
            this.changeID.Location = new System.Drawing.Point(177, 72);
            this.changeID.Name = "changeID";
            this.changeID.Size = new System.Drawing.Size(91, 23);
            this.changeID.TabIndex = 2;
            this.changeID.Text = "Change TitleID";
            this.changeID.UseVisualStyleBackColor = true;
            this.changeID.Click += new System.EventHandler(this.changeID_Click);
            // 
            // mediaID
            // 
            this.mediaID.Location = new System.Drawing.Point(71, 57);
            this.mediaID.MaxLength = 8;
            this.mediaID.Name = "mediaID";
            this.mediaID.Size = new System.Drawing.Size(100, 20);
            this.mediaID.TabIndex = 4;
            // 
            // titleID
            // 
            this.titleID.Location = new System.Drawing.Point(71, 22);
            this.titleID.MaxLength = 8;
            this.titleID.Name = "titleID";
            this.titleID.Size = new System.Drawing.Size(100, 20);
            this.titleID.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "TitleID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "MediaID:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.xex";
            this.openFileDialog1.Filter = "*.xex|*.xex";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 107);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mediaID);
            this.Controls.Add(this.titleID);
            this.Controls.Add(this.changeID);
            this.Controls.Add(this.addID);
            this.Controls.Add(this.openXex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "TitleID Changer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openXex;
        private System.Windows.Forms.Button addID;
        private System.Windows.Forms.Button changeID;
        private System.Windows.Forms.TextBox mediaID;
        private System.Windows.Forms.TextBox titleID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

