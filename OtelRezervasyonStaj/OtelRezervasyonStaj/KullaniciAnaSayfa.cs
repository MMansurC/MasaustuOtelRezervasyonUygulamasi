using System;
using System.Data.OleDb;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OtelRezervasyonStaj
{
    public partial class KullaniciAnaSayfa : Form
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
        public KullaniciAnaSayfa()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public string tc, adsoy;
        private DateTime BugunRezervIcin;
        private void taleple()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand talepp = new OleDbCommand("INSERT INTO Talep (Onay,AdSoyad,Cinsiyet,Yas,TCKimlik,Telefon,BaslangicT,BitisT,AdSoyad2,Cinsiyet2,Yas2,TCKimlik2,AdSoyad3,Cinsiyet3,Yas3,TCKimlik3,AdSoyad4,Cinsiyet4,Yas4,TCKimlik4,Gosterildi) VALUES (@onay,@adsoyad,@cinsiyet,@yas,@kimlik,@telefon,@baslangic,@bitis,@adsoyad2,@cinsiyet2,@yas2,@kimlik2,@adsoyad3,@cinsiyet3,@yas3,@kimlik3,@adsoyad4,@cinsiyet4,@yas4,@kimlik4,@gosterildi)", baglanti);
            talepp.Parameters.AddWithValue("onay", "REZERVE");
            talepp.Parameters.AddWithValue("adsoyad", AdSoyadBox.Text);
            if (cmbCinsiyet.SelectedIndex == 0)
            {
                talepp.Parameters.AddWithValue("cinsiyet", "Bay");
            }
            else if (cmbCinsiyet.SelectedIndex == 1)
            {
                talepp.Parameters.AddWithValue("cinsiyet", "Bayan");
            }
            talepp.Parameters.AddWithValue("yas", YasBox.Text);
            talepp.Parameters.AddWithValue("kimlik", TCKimlikBox.Text);
            talepp.Parameters.AddWithValue("telefon", TelefonBox.Text);
            talepp.Parameters.AddWithValue("baslangic", dtBaslangic.Value);
            talepp.Parameters.AddWithValue("bitis", dtBitis.Value);
            talepp.Parameters.AddWithValue("adsoyad2", AdSoyadBox2.Text);
            if (cmbCinsiyet2.SelectedIndex != -1)
            {
                if (cmbCinsiyet2.SelectedIndex == 0)
                {
                    talepp.Parameters.AddWithValue("cinsiyet2", "Bay");
                }
                else if (cmbCinsiyet2.SelectedIndex == 1)
                {
                    talepp.Parameters.AddWithValue("cinsiyet2", "Bayan");
                }
            }
            else
            {
                talepp.Parameters.AddWithValue("cinsiyet2", "");
            }
            
            talepp.Parameters.AddWithValue("yas2", YasBox2.Text);
            talepp.Parameters.AddWithValue("kimlik2", TCKimlikBox2.Text);
            talepp.Parameters.AddWithValue("adsoyad3", AdSoyadBox3.Text);
            if (cmbCinsiyet3.SelectedIndex != -1)
            {
                if (cmbCinsiyet3.SelectedIndex == 0)
                {
                    talepp.Parameters.AddWithValue("cinsiyet3", "Bay");
                }
                else if (cmbCinsiyet3.SelectedIndex == 1)
                {
                    talepp.Parameters.AddWithValue("cinsiyet3", "Bayan");
                }
            }
            else
            {
                talepp.Parameters.AddWithValue("cinsiyet3", "");
            }
            talepp.Parameters.AddWithValue("yas3", YasBox3.Text);
            talepp.Parameters.AddWithValue("kimlik3", TCKimlikBox3.Text);
            talepp.Parameters.AddWithValue("adsoyad4", AdSoyadBox4.Text);
            if (cmbCinsiyet4.SelectedIndex != -1)
            {
                if (cmbCinsiyet4.SelectedIndex == 0)
                {
                    talepp.Parameters.AddWithValue("cinsiyet4", "Bay");
                }
                else if (cmbCinsiyet4.SelectedIndex == 1)
                {
                    talepp.Parameters.AddWithValue("cinsiyet4", "Bayan");
                }
            }
            else
            {
                talepp.Parameters.AddWithValue("cinsiyet4", "");
            }
            talepp.Parameters.AddWithValue("yas4", YasBox4.Text);
            talepp.Parameters.AddWithValue("kimlik4", TCKimlikBox4.Text);
            talepp.Parameters.AddWithValue("gosterildi", "0");
            talepp.ExecuteNonQuery();
            baglanti.Close();
            int kisi_sayisi;
            if (AdSoyadBox4.Text != "")
            {
                kisi_sayisi = 4;
            }
            else if (AdSoyadBox3.Text != "")
            {
                kisi_sayisi = 3;
            }
            else if (AdSoyadBox2.Text != "")
            {
                kisi_sayisi = 2;
            }
            else
            {
                kisi_sayisi = 1;
            }
            int tutar = 60 + ((50 * kisi_sayisi) * Convert.ToInt32(tarihfarki));
            if (Baslangic.dil == 1)
            {
                MessageBox.Show("Rezervasyon talebi başarılı.\nRezervasyon talebiniz onaylanınca banka hesabınızdan " + tutar + " TL alınacaktır.");
            }
            else
            {
                MessageBox.Show("Your reservation request is successful\nWhen your request is approved, " + tutar + " TL will be debited from your bank account.");
            }
        }

        private void ClearAll()
        {
            AdSoyadBox.Text = adsoy;
            AdSoyadBox2.Text = "";
            AdSoyadBox3.Text = "";
            AdSoyadBox4.Text = "";
            cmbCinsiyet.SelectedIndex = -1;
            cmbCinsiyet2.SelectedIndex = -1;
            cmbCinsiyet3.SelectedIndex = -1;
            cmbCinsiyet4.SelectedIndex = -1;
            YasBox.Text = "";
            YasBox2.Text = "";
            YasBox3.Text = "";
            YasBox4.Text = "";
            TCKimlikBox.Text = tc;
            TCKimlikBox2.Text = "";
            TCKimlikBox3.Text = "";
            TCKimlikBox4.Text = "";
            TelefonBox.Text = "";
        }
        public void English()
        {
            label2.Text = "The BOF Hotel Rize, which draws notice with its awesome decor and unique construction, is Rize's newest hotel. Its architecture, which combines comfort and luxury, will make you feel special in a welcoming environment where hospitality service becomes an art form.\n\nThe Bof Hotel Rize has a variety of room types, multipurpose halls, a fitness club, and several activities.";
            bunifuFlatButton1.Text = "To Make a Reservation";
            bunifuFlatButton2.Text = "Reserve";
            label3.Text = "The BOF Hotel Rize";
            lblContact.Text = "CONTACT";
            CopyText.Text = "-   Copied";
            lblMusteriSayisi.Text = "Guest Count";
            lblMusteri1.Text = "Guest 1";
            lblMusteri2.Text = "Guest 2";
            lblMusteri3.Text = "Guest 3";
            lblMusteri4.Text = "Guest 4";
            lblAdSoyad.Text = "Name - Surname:";
            lblAdSoyad2.Text = "Name - Surname:";
            lblAdSoyad3.Text = "Name - Surname:";
            lblAdSoyad4.Text = "Name - Surname:";
            lblCinsiyet.Text = "Gender:";
            lblCinsiyet2.Text = "Gender:";
            lblCinsiyet3.Text = "Gender:";
            lblCinsiyet4.Text = "Gender:";
            lblYas.Text = "Age:";
            lblYas2.Text = "Age:";
            lblYas3.Text = "Age:";
            lblYas4.Text = "Age:";
            lblTCKimlik.Text = "ID Number:";
            lblTCKimlik2.Text = "ID Number:";
            lblTCKimlik3.Text = "ID Number:";
            lblTCKimlik4.Text = "ID Number:";
            lblTelefon.Text = "Phone Number:";
            Copied2.Text = "-   Copied";
            destek.Text = "NEED SUPPORT?";
            lblMevcutRezervasyon.Text = "Current Reservation";
            btnKayitSil.Text = "Delete Account";
            cmbCinsiyet.Items.Clear();
            cmbCinsiyet2.Items.Clear();
            cmbCinsiyet3.Items.Clear();
            cmbCinsiyet4.Items.Clear();
            cmbCinsiyet.Items.Add("Male");
            cmbCinsiyet2.Items.Add("Male");
            cmbCinsiyet3.Items.Add("Male");
            cmbCinsiyet4.Items.Add("Male");
            cmbCinsiyet.Items.Add("Female");
            cmbCinsiyet2.Items.Add("Female");
            cmbCinsiyet3.Items.Add("Female");
            cmbCinsiyet4.Items.Add("Female");
        }
        public void Turkish()
        {
            label2.Text = "Müthiş dekoru ve eşsiz yapısı ile dikkat çeken BOF Hotel Rize, Rize'nin en yeni otelidir. Konfor ve lüksü birleştiren mimarisi, misafirperverlik hizmetinin bir sanat haline geldiği samimi bir ortamda kendinizi özel hissetmenizi sağlayacaktır.\n\nBof Hotel Rize, çeşitli oda seçenekleri, çok amaçlı salonlar, sağlık kulübü ve birçok aktivite sunmaktadır.";
            bunifuFlatButton1.Text = "Rezervasyon Talep Et";
            bunifuFlatButton2.Text = "Rezervasyon Yap";
            label3.Text = "BOF Hotel Rize";
            lblContact.Text = "İLETİŞİM";
            CopyText.Text = "-   Kopyalandı";
            lblMusteriSayisi.Text = "Misafir Sayısı";
            lblMusteri1.Text = "Misafir 1";
            lblMusteri2.Text = "Misafir 2";
            lblMusteri3.Text = "Misafir 3";
            lblMusteri4.Text = "Misafir 4";
            lblAdSoyad.Text = "Adı Soyadı:";
            lblAdSoyad2.Text = "Adı Soyadı:";
            lblAdSoyad3.Text = "Adı Soyadı:";
            lblAdSoyad4.Text = "Adı Soyadı:";
            lblCinsiyet.Text = "Cinsiyet:";
            lblCinsiyet2.Text = "Cinsiyet:";
            lblCinsiyet3.Text = "Cinsiyet:";
            lblCinsiyet4.Text = "Cinsiyet:";
            lblYas.Text = "Yaş:";
            lblYas2.Text = "Yaş:";
            lblYas3.Text = "Yaş:";
            lblYas4.Text = "Yaş:";
            lblTCKimlik.Text = "T.C. Kimlik:";
            lblTCKimlik2.Text = "T.C. Kimlik:";
            lblTCKimlik3.Text = "T.C. Kimlik:";
            lblTCKimlik4.Text = "T.C. Kimlik:";
            lblTelefon.Text = "Telefon:";
            Copied2.Text = "-   Kopyalandı";
            destek.Text = "DESTEK";
            lblMevcutRezervasyon.Text = "Mevcut Rezervasyon";
            btnKayitSil.Text = "Hesabı Sil";
            cmbCinsiyet.Items.Clear();
            cmbCinsiyet2.Items.Clear();
            cmbCinsiyet3.Items.Clear();
            cmbCinsiyet4.Items.Clear();
            cmbCinsiyet.Items.Add("Bay");
            cmbCinsiyet2.Items.Add("Bay");
            cmbCinsiyet3.Items.Add("Bay");
            cmbCinsiyet4.Items.Add("Bay");
            cmbCinsiyet.Items.Add("Bayan");
            cmbCinsiyet2.Items.Add("Bayan");
            cmbCinsiyet3.Items.Add("Bayan");
            cmbCinsiyet4.Items.Add("Bayan");
        }

        private void KullaniciAnaSayfa_Load(object sender, EventArgs e)
        {
            bugun = DateTime.Now.ToShortDateString();
            dtBaslangic.Value = Convert.ToDateTime(bugun);
            dtBitis.Value = Convert.ToDateTime(bugun).AddDays(1);
            baslangicT = dtBaslangic.Value.ToShortDateString();
            bitisT = dtBitis.Value.ToShortDateString();
            dttarihfarki = Convert.ToDateTime(bitisT) - Convert.ToDateTime(baslangicT);
            tarihfarki = dttarihfarki.TotalDays.ToString();

            if (Baslangic.dil == 1)
            {
                Turkish();
            }
            else
            {
                English();
            }
            KalanSureKontrol();

            OnayKontrol();
            if (RezervButonGizle == 0)
            {
                RezerButonGetir();
            }
            else
            {
                RezervButonGotur();
            }

            if (KalanSurePanel.Visible == true)
            {
                BitisTarihiBul();
                if (BaslangicFromDB > BugunRezervIcin)
                {
                    RezerveKacGunKaldi();
                    timer3.Start();
                }
                else if (BitisFromDB < BugunRezervIcin)
                {

                }
                else
                {
                    timer2.Start();
                }
                
            }
            BaslangictaRezervK();


        }
        private string OnayliMi, GosterildiMi;
        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GeriTusu_Click(object sender, EventArgs e)
        {
            if (RezervasyonPanel.Visible == false)
            {
                if (Baslangic.dil == 1)
                {
                    DialogResult dialog = MessageBox.Show("Mevcut hesabınızdan çıkmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Baslangic baslangic = new Baslangic();
                        this.Hide();
                        baslangic.Show();
                    }
                    else
                    {

                    }
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Are you sure you want to sign out from your current account?", "Uyarı", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Baslangic baslangic = new Baslangic();
                        this.Hide();
                        baslangic.Show();
                    }
                    else
                    {

                    }
                }
            }
            else if (RezervasyonPanel.Visible == true)
            {
                RezervasyonPanel.Visible = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Resimler resimler = new Resimler();
            resimler.resim1.Visible = true;
            resimler.resim2.Visible = false;
            resimler.resim3.Visible = false;
            resimler.resim4.Visible = false;
            resimler.resim5.Visible = false;
            resimler.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Resimler resimler = new Resimler();
            resimler.resim1.Visible = false;
            resimler.resim2.Visible = true;
            resimler.resim3.Visible = false;
            resimler.resim4.Visible = false;
            resimler.resim5.Visible = false;
            resimler.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Resimler resimler = new Resimler();
            resimler.resim1.Visible = false;
            resimler.resim2.Visible = false;
            resimler.resim3.Visible = true;
            resimler.resim4.Visible = false;
            resimler.resim5.Visible = false;
            resimler.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Resimler resimler = new Resimler();
            resimler.resim1.Visible = false;
            resimler.resim2.Visible = false;
            resimler.resim3.Visible = false;
            resimler.resim4.Visible = true;
            resimler.resim5.Visible = false;
            resimler.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Resimler resimler = new Resimler();
            resimler.resim1.Visible = false;
            resimler.resim2.Visible = false;
            resimler.resim3.Visible = false;
            resimler.resim4.Visible = false;
            resimler.resim5.Visible = true;
            resimler.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Baslangic.dil = 1;
            Turkish();
            Odalbl_tr.Visible = true;
            Odalbl_en.Visible = false;

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Baslangic.dil = 2;
            English();
            Odalbl_tr.Visible = false;
            Odalbl_en.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(linkLabel1.Text);
            CopyText.Visible = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CopyText.Visible = false;
            Copied2.Visible = false;
            timer1.Stop();
        }

        DateTime dtbugun;
        string bugun, baslangicT, bitisT, tarihfarki;
        TimeSpan dttarihfarki;
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            dtbugun = DateTime.Now;
            bugun = dtbugun.ToShortDateString();



            RezervasyonPanel.Size = new Size(940, 642);
            ClearAll();
            if (numericUpDown1.Value == 1)
            {
                Musteri2Panel.Visible = false;
                Musteri3Panel.Visible = false;
                Musteri4Panel.Visible = false;
            }
            else if (numericUpDown1.Value == 2)
            {
                Musteri2Panel.Visible = true;
                Musteri3Panel.Visible = false;
                Musteri4Panel.Visible = false;
            }
            else if (numericUpDown1.Value == 3)
            {
                Musteri2Panel.Visible = true;
                Musteri3Panel.Visible = true;
                Musteri4Panel.Visible = false;
                Musteri3Panel.Location = new Point(270, 231);
            }
            else if (numericUpDown1.Value == 4)
            {
                Musteri2Panel.Visible = true;
                Musteri3Panel.Visible = true;
                Musteri4Panel.Visible = true;
                Musteri3Panel.Location = new Point(69, 231);

            }
            else
            {
                MessageBox.Show("İstenmeyen nümerik değer.", "HATA");
            }
            RezervasyonPanel.Visible = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(linkLabel2.Text);
            Copied2.Visible = true;
            timer1.Start();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Baslangic.dil = 2;
            English();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Baslangic.dil = 1;
            Turkish();
        }

        private void YasBox_TextChanged(object sender, EventArgs e)
        {
            if (YasBox.Text == "")
            {

            }
            else if (Convert.ToInt32(YasBox.Text) > 120)
            {
                YasBox.Text = "120";
            }
        }

        private void YasBox2_TextChanged(object sender, EventArgs e)
        {
            if (YasBox2.Text == "")
            {

            }
            else if (Convert.ToInt32(YasBox2.Text) > 120)
            {
                YasBox2.Text = "120";
            }
        }

        private void YasBox3_TextChanged(object sender, EventArgs e)
        {
            if (YasBox3.Text == "")
            {

            }
            else if (Convert.ToInt32(YasBox3.Text) > 120)
            {
                YasBox3.Text = "120";
            }
        }

        private void YasBox4_TextChanged(object sender, EventArgs e)
        {
            if (YasBox4.Text == "")
            {

            }
            else if (Convert.ToInt32(YasBox4.Text) > 120)
            {
                YasBox4.Text = "120";
            }
        }

        private void YasBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void YasBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void YasBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void YasBox4_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TCKimlikBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TCKimlikBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TCKimlikBox4_KeyPress(object sender, KeyPressEventArgs e)
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


        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            if (numericUpDown1.Value == 1)
            {
                if (AdSoyadBox.Text == "" || cmbCinsiyet.SelectedIndex == -1 || YasBox.Text == "" || TCKimlikBox.Text == "" || TelefonBox.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 1 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 1.", "Caution");
                    }

                }
                else if (Convert.ToInt32(YasBox.Text)<18)
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("On sekiz yaşından küçüklerin otel rezervasyonu yapmasına izin verilmez.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Hotel reservations are not permitted for minors under the age of eighteen.", "Caution");
                    }
                    
                }
                else
                {
                    taleple();
                    RezervasyonPanel.Visible = false;
                    RezervButonGotur();
                    KalanSurePanel.Visible = false;

                }

            }
            else if (numericUpDown1.Value == 2)
            {
                if (AdSoyadBox.Text == "" || cmbCinsiyet.SelectedIndex == -1 || YasBox.Text == "" || TCKimlikBox.Text == "" || TelefonBox.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 1 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 1.", "Caution");
                    }
                }
                else if (AdSoyadBox2.Text == "" || cmbCinsiyet2.SelectedIndex == -1 || YasBox2.Text == "" || TCKimlikBox2.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 2 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 2.", "Caution");
                    }
                }
                else
                {
                    taleple();
                    RezervasyonPanel.Visible = false;
                    RezervButonGotur();
                    KalanSurePanel.Visible = false;
                }
            }
            else if (numericUpDown1.Value == 3)
            {
                if (AdSoyadBox.Text == "" || cmbCinsiyet.SelectedIndex == -1 || YasBox.Text == "" || TCKimlikBox.Text == "" || TelefonBox.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 1 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 1.", "Caution");
                    }
                }
                else if (AdSoyadBox2.Text == "" || cmbCinsiyet2.SelectedIndex == -1 || YasBox2.Text == "" || TCKimlikBox2.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 2 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 2.", "Caution");
                    }
                }
                else if (AdSoyadBox3.Text == "" || cmbCinsiyet3.SelectedIndex == -1 || YasBox3.Text == "" || TCKimlikBox3.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 3 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 3.", "Caution");
                    }
                }
                else
                {
                    taleple();
                    RezervasyonPanel.Visible = false;
                    RezervButonGotur();
                    KalanSurePanel.Visible = false;
                }
            }
            else if (numericUpDown1.Value == 4)
            {
                if (AdSoyadBox.Text == "" || cmbCinsiyet.SelectedIndex == -1 || YasBox.Text == "" || TCKimlikBox.Text == "" || TelefonBox.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 1 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 1.", "Caution");
                    }
                }
                else if (AdSoyadBox2.Text == "" || cmbCinsiyet2.SelectedIndex == -1 || YasBox2.Text == "" || TCKimlikBox2.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 2 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 2.", "Caution");
                    }
                }
                else if (AdSoyadBox3.Text == "" || cmbCinsiyet3.SelectedIndex == -1 || YasBox3.Text == "" || TCKimlikBox3.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 3 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 3.", "Caution");
                    }
                }
                else if(AdSoyadBox4.Text == "" || cmbCinsiyet4.SelectedIndex == -1 || YasBox4.Text == "" || TCKimlikBox4.Text == "")
                {
                    if (Baslangic.dil == 1)
                    {
                        MessageBox.Show("Müşteri 4 için eksik bilgi.", "Uyarı");
                    }
                    else if (Baslangic.dil == 2)
                    {
                        MessageBox.Show("Missing information for guest 4.", "Caution");
                    }
                }
                else
                {
                    taleple();
                    RezervasyonPanel.Visible = false;
                    RezervButonGotur();
                    KalanSurePanel.Visible = false;
                }
            }
        }


        private void dtBitis_onValueChanged(object sender, EventArgs e)
        {
            if (dtBitis.Value < dtBaslangic.Value.AddDays(1))
            {
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Rezervasyon bitiş tarihi başlangıç tarihine eşit veya küçük olamaz.", "Uyarı");
                }
                else if (Baslangic.dil == 2)
                {
                    MessageBox.Show("It is not possible to make reservations for a past time.", "Caution");
                }
                dtBitis.Value = dtBaslangic.Value.AddDays(1);
            }
            bitisT = dtBitis.Value.ToShortDateString();
            dttarihfarki = Convert.ToDateTime(bitisT) - Convert.ToDateTime(baslangicT);
            tarihfarki = dttarihfarki.TotalDays.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < 1)
            {
                numericUpDown1.Value = 1;
            }
            else if (numericUpDown1.Value > 4)
            {
                numericUpDown1.Value = 4;
            }
        }

        private int Gun, saat, dakika;
        private void timer2_Tick(object sender, EventArgs e)
        {
            BugunRezervIcin = DateTime.Now;
            BitiseKalanSure = BitisFromDB - BugunRezervIcin;
            BitKalanS = Convert.ToString(Math.Truncate(BitiseKalanSure.TotalMinutes));
            ProgressBar1.Value = Convert.ToInt32(ToplamSure) - Convert.ToInt32(BitKalanS);
            Gun = Convert.ToInt32(BitKalanS)/1440;
            saat = (Convert.ToInt32(BitKalanS)%1440)/60;
            dakika = ((Convert.ToInt32(BitKalanS)%1440)%60);
            if (Convert.ToInt32(BitKalanS) / 1440 > 0)
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "" + Gun + " Gün " + saat + " Saat " + dakika + " Dakika";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "" + Gun + " Day(s) " + saat + " Hour(s) " + dakika + " Minute(s)";
                }
            }
            else if (Convert.ToInt32(BitKalanS) / 60 > 0)
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "" + saat + " Saat " + dakika + " Dakika";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "" + saat + " Hour(s) " + dakika + " Minute(s)";
                }
            }
            else
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "" + dakika + " Dakika";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "" + dakika + " Minute(s)";
                }
            }

            if (ProgressBar1.Value == ProgressBar1.MaximumValue)
            {
                timer2.Stop();
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Giriş süresi doldu.");
                }
                else if (Baslangic.dil == 2)
                {
                    MessageBox.Show("Login expired.");
                }
                Baslangic baslangic = new Baslangic();
                this.Hide();
                baslangic.Show();
            }
        }

        private void dtBaslangic_onValueChanged(object sender, EventArgs e)
        {
            if (dtBaslangic.Value < Convert.ToDateTime(bugun))
            {
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Geçmiş için rezervasyon yapmak mümkün değildir.", "Uyarı");
                }
                else if (Baslangic.dil == 2)
                {
                    MessageBox.Show("It is not possible to make reservations for a past time.", "Caution");
                }
                dtBaslangic.Value = Convert.ToDateTime(bugun);
                baslangicT = dtBaslangic.Value.ToShortDateString();
            }
            if (dtBaslangic.Value > dtBitis.Value)
            {
                dtBitis.Value = dtBaslangic.Value.AddDays(1);
            }
            baslangicT = dtBaslangic.Value.ToShortDateString();
            dttarihfarki = Convert.ToDateTime(bitisT) - Convert.ToDateTime(baslangicT);
            tarihfarki = dttarihfarki.TotalDays.ToString();
        }

        int RezervButonGizle = 0;
        private void OnayKontrol()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand onaykomut = new OleDbCommand("SELECT Onay,Gosterildi,Oda FROM Talep WHERE TCKimlik='" + tc + "'", baglanti);
            OleDbCommand gorulduyap = new OleDbCommand("UPDATE Talep SET Gosterildi='1' WHERE TCKimlik='" + tc + "'", baglanti);
            OleDbDataReader dr= onaykomut.ExecuteReader();
            if (dr.Read())
            {
                OnayliMi = dr[0].ToString();
                GosterildiMi = dr[1].ToString();
                Odalbl_tr.Text = "Oda: " + dr[2].ToString();
                Odalbl_en.Text = "Room: " + dr[2].ToString();
                if (Baslangic.dil == 1)
                {
                    Odalbl_tr.Visible = true;
                    Odalbl_en.Visible = false;
                }
                else if (Baslangic.dil == 2)
                {
                    Odalbl_tr.Visible = false;
                    Odalbl_en.Visible = true;
                }

                if (OnayliMi == "REDDET" && GosterildiMi == "0")
                {
                    gorulduyap.ExecuteNonQuery();
                    KullanıcıBildirim kullanıcıBildirim = new KullanıcıBildirim();
                    kullanıcıBildirim.tc = tc;
                    kullanıcıBildirim.BackColor = Color.FromArgb(255, 240, 200, 200);
                    if (Baslangic.dil == 1)
                    {
                        kullanıcıBildirim.lblOnay.Text = "Rezervasyon talebiniz reddedildi.";
                        kullanıcıBildirim.btnTamam.Text = "Tamam";
                    }
                    else if (Baslangic.dil == 2)
                    {
                        kullanıcıBildirim.lblOnay.Text = "Your reservation request was declined.";
                        kullanıcıBildirim.btnTamam.Text = "OK";
                    }
                    kullanıcıBildirim.ShowDialog();
                    OleDbCommand tlpsil = new OleDbCommand("DELETE * FROM Talep WHERE TCKimlik='" + tc + "'", baglanti);
                    tlpsil.ExecuteNonQuery();
                }
                else if (OnayliMi == "ONAYLI" && GosterildiMi == "0")
                {
                    gorulduyap.ExecuteNonQuery();
                    RezervButonGizle = 1;
                    KullanıcıBildirim kullanıcıBildirim = new KullanıcıBildirim();
                    kullanıcıBildirim.tc = tc;
                    kullanıcıBildirim.BackColor = Color.FromArgb(255, 200, 240, 200);
                    if (Baslangic.dil == 1)
                    {
                        kullanıcıBildirim.lblOnay.Text = "Rezervasyon talebiniz onaylandı.";
                    }
                    else if (Baslangic.dil == 2)
                    {
                        kullanıcıBildirim.lblOnay.Text = "Your reservation request was approved.";
                    }
                    kullanıcıBildirim.ShowDialog();
                }
                else
                {
                    RezervButonGizle = 1;
                }
            }
            else
            {
                RezervButonGizle = 0;
            }
            baglanti.Close();
        }

        private void RezerButonGetir()
        {
            numericUpDown1.Visible = true;
            label4.Visible = true;
            label6.Visible = true;
            dtBaslangic.Visible = true;
            dtBitis.Visible = true;
            lblMusteriSayisi.Visible = true;
            bunifuFlatButton1.Visible = true;
            KalanSurePanel.Visible = false;
        }

        private void RezervButonGotur()
        {
            numericUpDown1.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
            dtBaslangic.Visible = false;
            dtBitis.Visible = false;
            lblMusteriSayisi.Visible = false;
            bunifuFlatButton1.Visible = false;
            KalanSurePanel.Visible = true;
        }

        private DateTime BaslangicFromDB, BitisFromDB;

        private void btnKayitSil_Click(object sender, EventArgs e)
        {
            if (Baslangic.dil == 1)
            {
                DialogResult dialog = MessageBox.Show("Mevcut hesabınızı silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                    baglanti.Open();
                    OleDbCommand KullaniciSil = new OleDbCommand("DELETE * FROM KGiris WHERE TCKimlik='" + tc + "'", baglanti);
                    KullaniciSil.ExecuteNonQuery();
                    baglanti.Close();
                    Baslangic baslangic = new Baslangic();
                    this.Hide();
                    baslangic.Show();
                }
                else
                {

                }
            }
            else if (Baslangic.dil == 2)
            {
                DialogResult dialog = MessageBox.Show("Are you sure you want to delete your current account?", "Caution", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                    baglanti.Open();
                    OleDbCommand KullaniciSil = new OleDbCommand("DELETE * FROM KGiris WHERE TCKimlik='" + tc + "'", baglanti);
                    KullaniciSil.ExecuteNonQuery();
                    baglanti.Close();
                    Baslangic baslangic = new Baslangic();
                    this.Hide();
                    baslangic.Show();
                }
                else
                {

                }
            }
            
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            BugunRezervIcin = DateTime.Now;
            BaslangicaKalanSure = BaslangicFromDB - BugunRezervIcin;
            BasKalanS = Convert.ToString(Math.Truncate(BaslangicaKalanSure.TotalMinutes));
            Gun = Convert.ToInt32(BasKalanS) / 1440;
            saat = (Convert.ToInt32(BasKalanS) % 1440) / 60;
            dakika = ((Convert.ToInt32(BasKalanS) % 1440) % 60);
            if (Convert.ToInt32(BasKalanS) / 1440 > 0)
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "Rezervasyonunuz " + Gun + " Gün " + saat + " Saat " + dakika + " Dakika sonra başlayacak";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "Your reservation will start after " + Gun + " Day(s) " + saat + " Hour(s) " + dakika + " Minute(s)";
                }
            }
            else if (Convert.ToInt32(BasKalanS) / 60 > 0)
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "Rezervasyonunuz " + saat + " Saat " + dakika + " Dakika sonra başlayacak";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "Your reservation will start after " + saat + " Hour(s) " + dakika + " Minute(s)";
                }
            }
            else
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "Rezervasyonunuz " + dakika + " Dakika sonra başlayacak";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "Your reservation will start after " + dakika + " Minute(s)";
                }
            }

            if (ProgressBar1.Value == ProgressBar1.MaximumValue)
            {
                timer3.Stop();
                if (Baslangic.dil == 1)
                {
                    MessageBox.Show("Giriş süresi doldu.");
                }
                else if (Baslangic.dil == 2)
                {
                    MessageBox.Show("Login expired.");
                }
                Baslangic baslangic = new Baslangic();
                this.Hide();
                baslangic.Show();
            }
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            if (panel5.Visible == false)
            {
                panel5.Visible = true;
            }
            else if(panel5.Visible == true)
            {
                panel5.Visible = false;
            }
        }

        private TimeSpan BaslangicaKalanSure, BitiseKalanSure, surehesaplayici;
        private string BasKalanS, BitKalanS, ToplamSure;
        private void BitisTarihiBul()
        {
            BugunRezervIcin = DateTime.Now;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand bitissure = new OleDbCommand("SELECT BaslangicT,BitisT FROM Rezervasyon WHERE TCKimlik='" + tc + "'", baglanti);
            OleDbDataReader dr = bitissure.ExecuteReader();
            if (dr.Read())
            {
                BaslangicFromDB = Convert.ToDateTime(dr[0]);
                BitisFromDB = Convert.ToDateTime(dr[1]);
                BaslangicaKalanSure = BaslangicFromDB - BugunRezervIcin;
                BitiseKalanSure = BitisFromDB - BugunRezervIcin;
                surehesaplayici = BitisFromDB - BaslangicFromDB;
                BasKalanS = Convert.ToString(Math.Truncate(BaslangicaKalanSure.TotalMinutes));
                BitKalanS = Convert.ToString(Math.Truncate(BitiseKalanSure.TotalMinutes));
                ToplamSure = Convert.ToString(Math.Truncate(surehesaplayici.TotalMinutes));
                ProgressBar1.MaximumValue = Convert.ToInt32(ToplamSure);
                ProgressBar1.Value = Convert.ToInt32(ToplamSure)-Convert.ToInt32(BitKalanS);
                Gun = Convert.ToInt32(BitKalanS) / 1440;
                saat = (Convert.ToInt32(BitKalanS) % 1440) / 60;
                dakika = ((Convert.ToInt32(BitKalanS) % 1440) % 60);
                lblSure.Text = ("" + Convert.ToInt32(BitKalanS) / 1440);
                if (Convert.ToInt32(BitKalanS) / 1440 > 0)
                {
                    if (Baslangic.dil == 1)
                    {
                        lblSure.Text = "" + Gun + " Gün " + saat + " Saat " + dakika + " Dakika";
                    }
                    else if (Baslangic.dil == 2)
                    {
                        lblSure.Text = "" + Gun + " Day(s) " + saat + " Hour(s) " + dakika + " Minute(s)";
                    }
                }
                else if (Convert.ToInt32(BitKalanS) / 60 > 0)
                {
                    if (Baslangic.dil == 1)
                    {
                        lblSure.Text = "" + saat + " Saat " + dakika + " Dakika";
                    }
                    else if (Baslangic.dil == 2)
                    {
                        lblSure.Text = "" + saat + " Hour(s) " + dakika + " Minute(s)";
                    }
                }
                else
                {
                    if (Baslangic.dil == 1)
                    {
                        lblSure.Text = "" + dakika + " Dakika";
                    }
                    else if (Baslangic.dil == 2)
                    {
                        lblSure.Text = "" + dakika + " Minute(s)";
                    }
                }
                
            }
            else
            {

            }


        }

        private string bas, bit, ony, silinecektc;
        private void KalanSureKontrol()
        {

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("SELECT BaslangicT,BitisT,Onay FROM Rezervasyon WHERE TCKimlik='" + tc + "'", baglanti);
            OleDbDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                bas = Convert.ToString(dr[0]);
                bit = Convert.ToString(dr[1]);
                ony = Convert.ToString(dr[2]);
                if (ony == "DOLU")
                {
                    if (Convert.ToDateTime(bugun) > Convert.ToDateTime(bas) && Convert.ToDateTime(bugun) >= Convert.ToDateTime(bit))
                    {
                        OleDbCommand tlpsil = new OleDbCommand("DELETE * FROM Talep WHERE TCKimlik='" + tc + "'", baglanti);
                        tlpsil.ExecuteNonQuery();
                        OleDbCommand rezsil = new OleDbCommand("UPDATE Rezervasyon SET AdSoyad='',Onay='BOŞ',Cinsiyet='',Yas='',TCKimlik='',Telefon='',BaslangicT='" + baslangicT + "',BitisT='" + bitisT + "',AdSoyad2='',Cinsiyet2='',Yas2='',TCKimlik2='',AdSoyad3='',Cinsiyet3='',Yas3='',TCKimlik3='',AdSoyad4='',Cinsiyet4='',Yas4='',TCKimlik4='' WHERE TCKimlik='" + tc + "'", baglanti);
                        rezsil.ExecuteNonQuery();
                    }
                    else
                    {

                    }
                } 
            }
            OleDbCommand sorgu2 = new OleDbCommand("SELECT BitisT,TCKimlik FROM Talep", baglanti);
            dr = sorgu2.ExecuteReader();
            while (dr.Read())
            {
                bit = dr[0].ToString();
                silinecektc = dr[1].ToString();

                if (Convert.ToDateTime(bit) < Convert.ToDateTime(bugun))
                {
                    OleDbCommand tlpsil2 = new OleDbCommand("DELETE * FROM Talep WHERE TCKimlik='" + silinecektc + "'", baglanti);
                    tlpsil2.ExecuteNonQuery();
                }
            }
            baglanti.Close();
        }
        private void BaslangictaRezervK()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand talepicinsorgu = new OleDbCommand("SELECT Onay FROM Talep WHERE TCKimlik='" + tc + "'", baglanti);
            OleDbDataReader dr;
            dr = talepicinsorgu.ExecuteReader();
            if (dr.Read())
            {
                ony = dr[0].ToString();
                if (ony == "REZERVE")
                {
                    RezervasyonPanel.Visible = false;
                    RezervButonGotur();
                    KalanSurePanel.Visible = false;
                }
                else if (ony == "ONAYLI")
                {
                    
                    RezervButonGotur();
                }
                else if (ony == "REDDET")
                {

                }
            }
            else
            {

            }
            baglanti.Close();
        }
        private void RezerveKacGunKaldi()
        {
            lblSure.Location = new Point(0, 50);
            ProgressBar1.Visible = false;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            Gun = Convert.ToInt32(BasKalanS) / 1440;
            saat = (Convert.ToInt32(BasKalanS) % 1440) / 60;
            dakika = ((Convert.ToInt32(BasKalanS) % 1440) % 60);
            if (Convert.ToInt32(BasKalanS) / 1440 > 0)
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "Rezervasyonunuz " + Gun + " Gün " + saat + " Saat " + dakika + " Dakika sonra başlayacak";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "Your reservation will start after " + Gun + " Day(s) " + saat + " Hour(s) " + dakika + " Minute(s)";
                }
            }
            else if (Convert.ToInt32(BasKalanS) / 60 > 0)
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "Rezervasyonunuz " + saat + " Saat " + dakika + " Dakika sonra başlayacak";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "Your reservation will start after " + saat + " Hour(s) " + dakika + " Minute(s)";
                }
            }
            else
            {
                if (Baslangic.dil == 1)
                {
                    lblSure.Text = "Rezervasyonunuz " + dakika + " Dakika sonra başlayacak";
                }
                else if (Baslangic.dil == 2)
                {
                    lblSure.Text = "Your reservation will start after " + dakika + " Minute(s)";
                }
            }
            baglanti.Close();
        }
    }
}