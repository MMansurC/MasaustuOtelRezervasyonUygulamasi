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
    public partial class PersonelGiris : Form
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

        private void PersonelGiris_Load(object sender, EventArgs e)
        {
            if (Baslangic.dil == 1)
            {
                turkish();
            }
            else
            {
                english();
            }

            randomla();
        }
        
        public PersonelGiris()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public void english()
        {
            label2.Text = "Staff Login";
            label3.Text = "ID Number: ";
            label4.Text = "Password: ";
            label5.Text = "Security Code: ";
            bunifuFlatButton1.Text = "Log In";
        }
        public void turkish()
        {
            label2.Text = "Personel Girişi";
            label3.Text = "T.C Kimlik: ";
            label4.Text = "Şifre: ";
            label5.Text = "Güvenlik Kodu: ";
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Baslangic baslangic = new Baslangic();
            this.Hide();
            baslangic.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("SELECT TCKimlik,KullaniciSifre FROM PGiris WHERE TCKimlik=@tc AND KullaniciSifre=@sifre", baglanti);
            OleDbDataReader dr;
            sorgu.Parameters.AddWithValue("@tc", TCKimlikBox.Text);
            sorgu.Parameters.AddWithValue("@sifre", SifreBox.Text);
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
                    OleDbCommand personel = new OleDbCommand("SELECT AdSoyad FROM PGiris WHERE TCKimlik='" + TCKimlikBox.Text + "'", baglanti);
                    dr = personel.ExecuteReader();
                    if (dr.Read())
                    {
                        PersonelAnaSayfa personelAnaSayfa = new PersonelAnaSayfa();
                        personelAnaSayfa.PersonelAdi.Text = dr[0].ToString();
                        this.Hide();
                        personelAnaSayfa.Show();
                    }
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

        private void KayitLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonelKayit personelKayit = new PersonelKayit();
            this.Hide();
            personelKayit.Show();
        }

        private void KullaniciAdiBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void GvnlkBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void IngilizceDil_Click(object sender, EventArgs e)
        {
            Baslangic.dil = 2;
            english();
        }

        private void TurkceDil_Click(object sender, EventArgs e)
        {
            Baslangic.dil = 1;
            turkish();
        }


    }
}
