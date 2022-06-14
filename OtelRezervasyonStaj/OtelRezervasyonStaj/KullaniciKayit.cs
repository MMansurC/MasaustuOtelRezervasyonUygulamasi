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
    public partial class KullaniciKayit : Form
    {
        OleDbCommand komut;
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
        
        public KullaniciKayit()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public void English()
        {
            label2.Text = "Guest Registration";
            label3.Text = "ID Number: ";
            label4.Text = "Password: ";
            label5.Text = "Confirm Password: ";
            label6.Text = "Security Code: ";
            label7.Text = "Name - Surname: ";
            bunifuFlatButton1.Text = "Sign In";
        }
        public void Turkish()
        {
            label2.Text = "Misafir Kayıt";
            label3.Text = "T.C. Kimlik: ";
            label4.Text = "Şifre: ";
            label5.Text = "Şifreyi Onayla: ";
            label6.Text = "Güvenlik Kodu: ";
            label7.Text = "Adı Soyadı: ";
            bunifuFlatButton1.Text = "Kayıt Ol";
        }
        private void randomla()
        {
            Random rastgele = new Random();
            int sayi = rastgele.Next(1000, 9999);
            Gvnlk.Text = sayi.ToString();
            GvnlkBox.Text = "";
            SifreBox.Text = "";
            SifreTekrar.Text = "";
        }




        private void KullaniciKayit_Load(object sender, EventArgs e)
        {
            if (Baslangic.dil == 1)
            {
                Turkish();
            }
            else
            {
                English();
            }

            Random rastgele = new Random();
            int sayi;
            sayi = rastgele.Next(1000, 9999);
            Gvnlk.Text = sayi.ToString();
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Baslangic.dil = 2;
            English();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Baslangic.dil = 1;
            Turkish();
        }

        private void GeriTusu_Click(object sender, EventArgs e)
        {
            KullaniciGiris kullaniciGiris = new KullaniciGiris();
            this.Hide();
            kullaniciGiris.Show();
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            string sorgu = "INSERT INTO KGiris (TCKimlik,KullaniciSifre,AdSoyad) VALUES(@tc,@sifre,@ads)";
            komut = new OleDbCommand(sorgu, baglanti);
            if (AdSoyadBox.Text == "")
            {
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Ad soyad girişi boş bırakılamaz.", "Kayıt Hatası");
                }
                else
                {
                    MessageBox.Show("Name-Surname cannot be blank.", "Registration Error");
                }
            }
            else if (TCKimlikBox.Text == "")
            {
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("T.C. kimlik numarası boş bırakılamaz.", "Kayıt Hatası");
                    randomla();
                }
                else
                {
                    MessageBox.Show("ID number cannot be blank.", "Registration Error");
                    randomla();
                }
            }
            else if (SifreBox.Text == "")
            {
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Şifre boş bırakılamaz.", "Kayıt Hatası");
                    randomla();
                }
                else
                {
                    MessageBox.Show("Password cannot be blank.", "Registration Error");
                    randomla();
                }
            }
            else if (SifreBox.Text != SifreTekrar.Text)
            {
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Şifreler eşleşmiyor.", "Kayıt Hatası");
                    randomla();
                }
                else
                {
                    MessageBox.Show("Passwords do not match.", "Registration Error");
                    randomla();
                }
            }
            else if (GvnlkBox.Text == "")
            {
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Güvenlik kodu boş bırakılamaz.", "Kayıt Hatası");
                    randomla();
                }
                else
                {
                    MessageBox.Show("Security code cannot be blank.", "Registration Error");
                    randomla();
                }
            }
            else if (GvnlkBox.Text != Gvnlk.Text)
            {
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Güvenlik kodunu hatalı girdiniz.", "Kayıt Hatası");
                    randomla();
                }
                else
                {
                    MessageBox.Show("The security code is incorrect.", "Registration Error");
                    randomla();
                }
            }
            else
            {
                komut.Parameters.AddWithValue("@tc", TCKimlikBox.Text);
                komut.Parameters.AddWithValue("@sifre", SifreBox.Text);
                komut.Parameters.AddWithValue("@ads", AdSoyadBox.Text);
                komut.ExecuteNonQuery();
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show(TCKimlikBox.Text + " T.C. kimlik numarası ile sisteme kayıt olundu.");
                }
                else
                {
                    MessageBox.Show("You have registered into the system with the ID number " + TCKimlikBox.Text + ".");
                }
                TCKimlikBox.ResetText();
                SifreBox.ResetText();
                SifreTekrar.ResetText();
                baglanti.Close();
                KullaniciGiris kullaniciGiris = new KullaniciGiris();
                this.Hide();
                kullaniciGiris.Show();
            }
        }

        private void GvnlkBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TCKimlikBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void SifreBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void SifreTekrar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }
    }
}
