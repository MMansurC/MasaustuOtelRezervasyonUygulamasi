using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OtelRezervasyonStaj
{
    public partial class KullanıcıBildirim : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        public KullanıcıBildirim()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public string tc;

        private void MusteriBildirim_Load(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand sebepsor = new OleDbCommand("SELECT Sebep FROM Talep WHERE TCKimlik='" + tc + "'", baglanti);
            OleDbDataReader dr = sebepsor.ExecuteReader();
            if (dr.Read())
            {
                lblSebep.Text = dr[0].ToString();
            }
            else
            {
                MessageBox.Show("TC Kimlik hatası (Kullanıcı Bildirim)");
            }

            baglanti.Close();
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
