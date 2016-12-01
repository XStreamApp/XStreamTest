using MetroFramework.Forms;

namespace XStreamTest
{
    partial class mainForm : MetroForm
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
            this.serverTile = new MetroFramework.Controls.MetroTile();
            this.clientTile = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // serverTile
            // 
            this.serverTile.ActiveControl = null;
            this.serverTile.Location = new System.Drawing.Point(23, 233);
            this.serverTile.Name = "serverTile";
            this.serverTile.Size = new System.Drawing.Size(254, 51);
            this.serverTile.TabIndex = 0;
            this.serverTile.Text = "Server Mode";
            this.serverTile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.serverTile.UseSelectable = true;
            this.serverTile.Click += new System.EventHandler(this.serverTile_Click);
            // 
            // clientTile
            // 
            this.clientTile.ActiveControl = null;
            this.clientTile.Location = new System.Drawing.Point(23, 290);
            this.clientTile.Name = "clientTile";
            this.clientTile.Size = new System.Drawing.Size(254, 51);
            this.clientTile.TabIndex = 1;
            this.clientTile.Text = "Client Mode";
            this.clientTile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.clientTile.UseSelectable = true;
            this.clientTile.Click += new System.EventHandler(this.clientTile_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 400);
            this.Controls.Add(this.clientTile);
            this.Controls.Add(this.serverTile);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Text = "XStream";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTile serverTile;
        private MetroFramework.Controls.MetroTile clientTile;
    }
}