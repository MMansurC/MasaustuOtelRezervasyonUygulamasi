
namespace OtelRezervasyonStaj
{
    partial class KullanıcıBildirim
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
            this.lblOnay = new System.Windows.Forms.Label();
            this.btnTamam = new System.Windows.Forms.Button();
            this.lblSebep = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblOnay
            // 
            this.lblOnay.BackColor = System.Drawing.Color.Transparent;
            this.lblOnay.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOnay.Location = new System.Drawing.Point(31, 9);
            this.lblOnay.Name = "lblOnay";
            this.lblOnay.Size = new System.Drawing.Size(350, 25);
            this.lblOnay.TabIndex = 16;
            this.lblOnay.Text = "Durum";
            this.lblOnay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnTamam
            // 
            this.btnTamam.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTamam.Location = new System.Drawing.Point(136, 144);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(140, 40);
            this.btnTamam.TabIndex = 20;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.UseVisualStyleBackColor = true;
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
            // 
            // lblSebep
            // 
            this.lblSebep.Location = new System.Drawing.Point(31, 41);
            this.lblSebep.Name = "lblSebep";
            this.lblSebep.Size = new System.Drawing.Size(350, 97);
            this.lblSebep.TabIndex = 19;
            this.lblSebep.Text = "Sebep";
            this.lblSebep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KullanıcıBildirim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(412, 192);
            this.Controls.Add(this.lblSebep);
            this.Controls.Add(this.btnTamam);
            this.Controls.Add(this.lblOnay);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KullanıcıBildirim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MusteriBildirim";
            this.Load += new System.EventHandler(this.MusteriBildirim_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblOnay;
        private System.Windows.Forms.Label lblSebep;
        public System.Windows.Forms.Button btnTamam;
    }
}