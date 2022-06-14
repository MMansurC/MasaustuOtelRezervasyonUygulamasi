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
    public partial class KullaniciGiris : System.Windows.Forms.Form
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

        public KullaniciGiris()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        public void English()
        {
            label2.Text = "Guest Login";
            label3.Text = "ID Number: ";
            label4.Text = "Password: ";
            label5.Text = "Security Code: ";
            bunifuFlatButton1.Text = "Log In";
            label6.Text = "Don't have an account yet?";
            linkLabel1.Text = "Register";
        }
        public void Turkish()
        {
            label2.Text = "Misafir Girişi";
            label3.Text = "T.C. Kimlik: ";
            label4.Text = "Şifre: ";
            label5.Text = "Güvenlik Kodu: ";
            label6.Text = "Henüz hesabınız yok mu?";
            linkLabel1.Text = "Kayıt ol";
            bunifuFlatButton1.Text = "Giriş Yap";
        }
        private void randomla()
        {
            Random rastgele = new Random();
            int sayi = rastgele.Next(1000, 9999);
            Gvnlk.Text = sayi.ToString();
            GvnlkBox.Text = "";
            SifreBox.Text = "";
        }

        private void KullaniciGiris_Load(object sender, EventArgs e)
        {
            if (Baslangic.dil == 1)
            {
                Turkish();
            }
            else
            {
                English();
            }

            randomla();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Baslangic.dil = 1;
            Turkish();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Baslangic.dil = 2;
            English();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Baslangic baslangic = new Baslangic();
            this.Hide();
            baslangic.Show();
        }

        string MustName;
        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("SELECT TCKimlik,KullaniciSifre FROM KGiris WHERE TCKimlik=@tc AND KullaniciSifre=@sifre", baglanti);
            OleDbCommand musteri = new OleDbCommand("SELECT AdSoyad FROM KGiris WHERE TCKimlik='" + TCKimlikBox.Text + "'", baglanti);
            sorgu.Parameters.AddWithValue("@tc", TCKimlikBox.Text);
            sorgu.Parameters.AddWithValue("@sifre", SifreBox.Text);
            OleDbDataReader dr;
            dr = sorgu.ExecuteReader();
            if (dr.Read())
            {
                if (GvnlkBox.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Güvenlik kodu boş bırakılamaz.");
                    }
                    else
                    {
                        MessageBox.Show("Security code cannot be blank.");
                    }
                    randomla();
                }
                else if (GvnlkBox.Text != Gvnlk.Text)
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Güvenlik kodunu hatalı girdiniz.");
                    }
                    else
                    {
                        MessageBox.Show("The security code is incorrect.");
                    }
                    randomla();
                }
                else
                {
                    dr = musteri.ExecuteReader();
                    if (dr.Read())
                    {
                        MustName = dr[0].ToString();
                    }
                    KullaniciAnaSayfa kullaniciAnaSayfa = new KullaniciAnaSayfa();
                    kullaniciAnaSayfa.tc = TCKimlikBox.Text;
                    kullaniciAnaSayfa.adsoy= MustName;
                    kullaniciAnaSayfa.KullaniciAdi.Text = MustName;
                    this.Hide();
                    kullaniciAnaSayfa.Show();
                }
            }
            else
            {
                baglanti.Close();
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("T.C. kimlik numarası veya parola yanlış.");
                }
                else
                {
                    MessageBox.Show("Incorrect ID number or password.");
                }
                randomla();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KullaniciKayit kullaniciKayit = new KullaniciKayit();
            this.Hide();
            kullaniciKayit.Show();
        }

        private void KullaniciAdiBox_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = (e.KeyChar == (char)Keys.Space);
        }
    }
}
