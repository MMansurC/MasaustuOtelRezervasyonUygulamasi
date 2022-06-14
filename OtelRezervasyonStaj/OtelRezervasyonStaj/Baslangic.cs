using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Data.OleDb;

namespace OtelRezervasyonStaj
{
    public partial class Baslangic : System.Windows.Forms.Form
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

        public Baslangic()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public static int dil = 1;


        public void English()
        {
            bunifuFlatButton2.Text = "Staff Login";
            bunifuFlatButton1.Text = "Guest Login";

        }
        public void Turkish()
        {
            bunifuFlatButton2.Text = "Personel Girişi";
            bunifuFlatButton1.Text = "Misafir Girişi";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (dil == 1)
            {
                Turkish();
            }
            else
            {
                English();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            KullaniciGiris kullaniciGiris = new KullaniciGiris();
            this.Hide();
            kullaniciGiris.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            PersonelGiris personelGiris = new PersonelGiris();
            this.Hide();
            personelGiris.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            dil = 2;
            English();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            dil = 1;
            Turkish();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\OneDrive\\Masaüstü\\RezervOtelDatabase.accdb");
            baglanti.Open();
            MessageBox.Show("a");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\OneDrive\\Masaüstü\\RezervOtelDatabase.accdb");
            baglanti.Close();
            MessageBox.Show("b");
        }
    }
}
