namespace TicariOtomasyon
{
    partial class FrmNotDetay
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
            this.txtDetay = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtDetay
            // 
            this.txtDetay.BackColor = System.Drawing.SystemColors.Info;
            this.txtDetay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetay.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtDetay.Location = new System.Drawing.Point(0, 0);
            this.txtDetay.Name = "txtDetay";
            this.txtDetay.ReadOnly = true;
            this.txtDetay.Size = new System.Drawing.Size(810, 458);
            this.txtDetay.TabIndex = 0;
            this.txtDetay.Text = "";
            // 
            // FrmNotDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(810, 458);
            this.Controls.Add(this.txtDetay);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNotDetay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NOT DETAY";
            this.Load += new System.EventHandler(this.FrmNotDetay_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtDetay;
    }
}