﻿namespace PoetryBot4200
{
    partial class MainMenu
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
            this.btnGetFile_Source = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetFile_Destination = new System.Windows.Forms.Button();
            this.txtSourceStats = new System.Windows.Forms.TextBox();
            this.txtDestinationStats = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGetFile_Source
            // 
            this.btnGetFile_Source.Location = new System.Drawing.Point(12, 25);
            this.btnGetFile_Source.Name = "btnGetFile_Source";
            this.btnGetFile_Source.Size = new System.Drawing.Size(75, 23);
            this.btnGetFile_Source.TabIndex = 0;
            this.btnGetFile_Source.Text = "Get File";
            this.btnGetFile_Source.UseVisualStyleBackColor = true;
            this.btnGetFile_Source.Click += new System.EventHandler(this.btnGetFile_Source_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add Source Texts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Add Destination Texts";
            // 
            // btnGetFile_Destination
            // 
            this.btnGetFile_Destination.Location = new System.Drawing.Point(12, 86);
            this.btnGetFile_Destination.Name = "btnGetFile_Destination";
            this.btnGetFile_Destination.Size = new System.Drawing.Size(75, 23);
            this.btnGetFile_Destination.TabIndex = 2;
            this.btnGetFile_Destination.Text = "Get File";
            this.btnGetFile_Destination.UseVisualStyleBackColor = true;
            this.btnGetFile_Destination.Click += new System.EventHandler(this.btnGetFile_Destination_Click);
            // 
            // txtSourceStats
            // 
            this.txtSourceStats.Location = new System.Drawing.Point(93, 28);
            this.txtSourceStats.Name = "txtSourceStats";
            this.txtSourceStats.Size = new System.Drawing.Size(193, 20);
            this.txtSourceStats.TabIndex = 4;
            // 
            // txtDestinationStats
            // 
            this.txtDestinationStats.Location = new System.Drawing.Point(93, 88);
            this.txtDestinationStats.Name = "txtDestinationStats";
            this.txtDestinationStats.Size = new System.Drawing.Size(193, 20);
            this.txtDestinationStats.TabIndex = 5;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 345);
            this.Controls.Add(this.txtDestinationStats);
            this.Controls.Add(this.txtSourceStats);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGetFile_Destination);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetFile_Source);
            this.Name = "MainMenu";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetFile_Source;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetFile_Destination;
        private System.Windows.Forms.TextBox txtSourceStats;
        private System.Windows.Forms.TextBox txtDestinationStats;
    }
}
