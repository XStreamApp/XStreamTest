using System;
using MetroFramework.Forms;
namespace XStreamTest
{
    partial class ServerForm : MetroForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.backLink = new MetroFramework.Controls.MetroLink();
            this.tilesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // backLink
            // 
            this.backLink.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backLink.BackgroundImage")));
            this.backLink.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backLink.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.backLink.ForeColor = System.Drawing.Color.Black;
            this.backLink.Location = new System.Drawing.Point(24, 18);
            this.backLink.Name = "backLink";
            this.backLink.Size = new System.Drawing.Size(35, 34);
            this.backLink.Style = MetroFramework.MetroColorStyle.Teal;
            this.backLink.TabIndex = 0;
            this.backLink.Text = "Back";
            this.backLink.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.backLink.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.backLink.UseSelectable = true;
            this.backLink.Click += new System.EventHandler(this.backLink_Click);
            // 
            // tilesPanel
            // 
            this.tilesPanel.Location = new System.Drawing.Point(52, 82);
            this.tilesPanel.Name = "tilesPanel";
            this.tilesPanel.Size = new System.Drawing.Size(225, 286);
            this.tilesPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connected Devices";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(52, 374);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(225, 20);
            this.textBox1.TabIndex = 3;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(300, 400);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tilesPanel);
            this.Controls.Add(this.backLink);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Server";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLink backLink;
        private System.Windows.Forms.FlowLayoutPanel tilesPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}