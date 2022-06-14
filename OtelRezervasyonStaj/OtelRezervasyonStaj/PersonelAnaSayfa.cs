using System;
using System.Data.OleDb;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OtelRezervasyonStaj
{
    public partial class PersonelAnaSayfa : Form
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
        public PersonelAnaSayfa()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        DateTime dtbugun;
        TimeSpan tarihfarki;
        string bugun, baslangicT, bitisT, odacik;

        private void ClearAll()
        {
            AdSoyadBox.Clear();
            AdSoyadBox2.Clear();
            AdSoyadBox3.Clear();
            AdSoyadBox4.Clear();
            cmbCinsiyet.SelectedIndex = -1;
            cmbCinsiyet2.SelectedIndex = -1;
            cmbCinsiyet3.SelectedIndex = -1;
            cmbCinsiyet4.SelectedIndex = -1;
            YasBox.Clear();
            YasBox2.Clear();
            YasBox3.Clear();
            YasBox4.Clear();
            TCKimlikBox.Clear();
            TCKimlikBox2.Clear();
            TCKimlikBox3.Clear();
            TCKimlikBox4.Clear();
            TelefonBox.Clear();
            panelclear();
            dtBaslangic.Value = Convert.ToDateTime(bugun);
            dtBitis.Value = Convert.ToDateTime(bugun).AddDays(1);
        }
        private void panelclear()
        {
            Musteri2Panel.Visible = false;
            Musteri3Panel.Visible = false;
            Musteri4Panel.Visible = false;
        }
        string onayli;
        int tutar, kisi_sayisi;
        private void OdaGetir(string odacik)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand ole1 = new OleDbCommand("SELECT AdSoyad,Cinsiyet,Yas,TCKimlik,Telefon,BaslangicT,BitisT,Onay,AdSoyad2,AdSoyad3,AdSoyad4,Cinsiyet2,Cinsiyet3,Cinsiyet4,Yas2,Yas3,Yas4,TCKimlik2,TCKimlik3,TCKimlik4 FROM Rezervasyon WHERE Oda='" + odacik + "'", baglanti);
            OleDbDataReader dr = ole1.ExecuteReader();
            if (dr.Read())
            {
                AdSoyadBox.Text = dr[0].ToString();
                cmbCinsiyet.SelectedItem = dr[1].ToString();
                YasBox.Text = dr[2].ToString();
                TCKimlikBox.Text = dr[3].ToString();
                TelefonBox.Text = dr[4].ToString();
                dtBaslangic.Value = Convert.ToDateTime(dr[5]);
                dtBitis.Value = Convert.ToDateTime(dr[6]);
                onayli = dr[7].ToString();
                AdSoyadBox2.Text = dr[8].ToString();
                AdSoyadBox3.Text = dr[9].ToString();
                AdSoyadBox4.Text = dr[10].ToString();
                cmbCinsiyet2.SelectedItem = dr[11].ToString();
                cmbCinsiyet3.SelectedItem = dr[12].ToString();
                cmbCinsiyet4.SelectedItem = dr[13].ToString();
                YasBox2.Text = dr[14].ToString();
                YasBox3.Text = dr[15].ToString();
                YasBox4.Text = dr[16].ToString();
                TCKimlikBox2.Text = dr[17].ToString();
                TCKimlikBox3.Text = dr[18].ToString();
                TCKimlikBox4.Text = dr[19].ToString();
                baslangicT = dtBaslangic.Value.ToShortDateString();
                bitisT = dtBitis.Value.ToShortDateString();
                if (onayli == "BOŞ")
                {
                    btnOnayla.Text = "Onayla";
                    btnBosalt.Visible = false;
                }
                else if (onayli == "DOLU")
                {
                    btnOnayla.Text = "Güncelle";
                    btnBosalt.Visible = true;
                }

            }
            baglanti.Close();
        }
        private void PersonelSayisiBul()
        {
            int kalanPersonel = 1;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand personelsay = new OleDbCommand("SELECT COUNT(*) FROM PGiris", baglanti);
            OleDbDataReader dr = personelsay.ExecuteReader();
            if (dr.Read())
            {
                kalanPersonel = Convert.ToInt32(dr[0]);
            }

            if (kalanPersonel == 1)
            {
                btnKayitSil.Enabled = false;
                btnKayitSil.ForeColor = Color.White;
                btnKayitSil.Text = "Tek Personelsiniz";
            }
            baglanti.Close();
        }

        int talepx;
        private void talepsayisibul()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();

            OleDbCommand talep = new OleDbCommand("SELECT COUNT(*) FROM Talep WHERE Onay='REZERVE'", baglanti);
            OleDbDataReader dr;
            dr = talep.ExecuteReader();
            if (dr.Read())
            {
                ToplamTalepSayisi.Text = Convert.ToString(dr[0]);
                talepx = Convert.ToInt32(dr[0]);
            }

            baglanti.Close();
        }

        string odabuton;
        public void DoluBosKontrol()
        {
            for (int i = 1; i <= 18; i++)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand DoluBosKont = new OleDbCommand("SELECT Oda,Onay,OdaButon FROM Rezervasyon WHERE Kimlik=" + i + "", baglanti);
                OleDbDataReader dr = DoluBosKont.ExecuteReader();
                if (dr.Read())
                {
                    odacik = dr[0].ToString();
                    on = dr[1].ToString();
                    odabuton = dr[2].ToString();

                    this.Controls.Find(odabuton, true)[0].Text = odacik + "\n" + on;
                    if (on == "DOLU")
                    {
                        this.Controls.Find(odabuton, true)[0].BackColor = Color.FromArgb(255, 240, 187, 204);
                    }
                    else if (on == "BOŞ")
                    {
                        this.Controls.Find(odabuton, true)[0].BackColor = SystemColors.AppWorkspace;
                    }

                }


                baglanti.Close();
            }
        }

        public void HangiOdaDoluysaDisable()
        {
            for (int i = 1; i <= 18; i++)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand DoluysaDisable = new OleDbCommand("SELECT OdaButon FROM Rezervasyon WHERE Kimlik=" + i + "", baglanti);
                OleDbDataReader dr = DoluysaDisable.ExecuteReader();
                if (dr.Read())
                {
                    odabuton = dr[0].ToString();
                    if (this.Controls.Find(odabuton, true)[0].BackColor == Color.FromArgb(255, 240, 187, 204))
                    {
                        this.Controls.Find(odabuton, true)[0].Enabled = false;
                    }
                }
                baglanti.Close();
            }
        }

        public void OdalarEnabled()
        {
            for (int i = 1; i <= 18; i++)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand DoluysaDisable = new OleDbCommand("SELECT OdaButon FROM Rezervasyon WHERE Kimlik=" + i + "", baglanti);
                OleDbDataReader dr = DoluysaDisable.ExecuteReader();
                if (dr.Read())
                {
                    odabuton = dr[0].ToString();
                    this.Controls.Find(odabuton, true)[0].Enabled = true;
                }
                baglanti.Close();
            }
        }

        string bas, bit, ony;
        private void KalanSureKontrol(string odacik)
        {

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("SELECT BaslangicT,BitisT,Onay FROM Rezervasyon WHERE Oda='" + odacik + "'", baglanti);
            OleDbDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                bas = Convert.ToString(dr[0]);
                bit = Convert.ToString(dr[1]);
                ony = Convert.ToString(dr[2]);
                if (ony == "BOŞ")
                {
                    OleDbCommand tarihduzenle = new OleDbCommand("UPDATE Rezervasyon SET BaslangicT='" + baslangicT + "',BitisT='" + bitisT + "' WHERE Oda='" + odacik + "'", baglanti);
                    tarihduzenle.ExecuteNonQuery();
                }
                else if (ony == "DOLU")
                {
                    if (Convert.ToDateTime(bugun) > Convert.ToDateTime(bas) && Convert.ToDateTime(bugun) >= Convert.ToDateTime(bit))
                    {
                        OleDbCommand rezsil = new OleDbCommand("UPDATE Rezervasyon SET AdSoyad='',Onay='BOŞ',Cinsiyet='',Yas='',TCKimlik='',Telefon='',BaslangicT='" + baslangicT + "',BitisT='" + bitisT + "',AdSoyad2='',Cinsiyet2='',Yas2='',TCKimlik2='',AdSoyad3='',Cinsiyet3='',Yas3='',TCKimlik3='',AdSoyad4='',Cinsiyet4='',Yas4='',TCKimlik4='' WHERE Oda='" + odacik + "'", baglanti);
                        rezsil.ExecuteNonQuery();

                    }
                    else
                    {

                    }
                }
            }




            baglanti.Close();
        }

        public void OdaDuzelt()
        {
            for (int i = 1; i <= 18; i++)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand odaduzelt = new OleDbCommand("SELECT Oda,Onay FROM Rezervasyon WHERE Kimlik=" + i + "", baglanti);
                OleDbDataReader dr = odaduzelt.ExecuteReader();
                if (dr.Read())
                {
                    odacik = dr[0].ToString();
                    on = dr[1].ToString();
                    KalanSureKontrol(odacik);
                }
                baglanti.Close();
            }
        }

        private string cinsiyet1, cinsiyet2, cinsiyet3, cinsiyet4;
        private void odaguncelle(string odacik)
        {
            if (cmbCinsiyet.SelectedIndex == -1)
            {
                cinsiyet1 = "";
            }
            else
            {
                cinsiyet1 = Convert.ToString(cmbCinsiyet.SelectedItem);
            }
            if (cmbCinsiyet2.SelectedIndex == -1)
            {
                cinsiyet2 = "";
            }
            else
            {
                cinsiyet2 = Convert.ToString(cmbCinsiyet2.SelectedItem);
            }
            if (cmbCinsiyet3.SelectedIndex == -1)
            {
                cinsiyet3 = "";
            }
            else
            {
                cinsiyet3 = Convert.ToString(cmbCinsiyet3.SelectedItem);
            }
            if (cmbCinsiyet4.SelectedIndex == -1)
            {
                cinsiyet4 = "";
            }
            else
            {
                cinsiyet4 = Convert.ToString(cmbCinsiyet4.SelectedItem);
            }
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand odagunc = new OleDbCommand("UPDATE Rezervasyon SET Onay='DOLU',AdSoyad='" + AdSoyadBox.Text + "',AdSoyad2='" + AdSoyadBox2.Text + "',AdSoyad3='" + AdSoyadBox3.Text + "',AdSoyad4='" + AdSoyadBox4.Text + "',Cinsiyet='" + cinsiyet1 + "',Cinsiyet2='" + cinsiyet2 + "',Cinsiyet3='" + cinsiyet3 + "',Cinsiyet4='" + cinsiyet4 + "',Yas='" + YasBox.Text + "',Yas2='" + YasBox2.Text + "',Yas3='" + YasBox3.Text + "',Yas4='" + YasBox4.Text + "',TCKimlik='" + TCKimlikBox.Text + "',TCKimlik2='" + TCKimlikBox2.Text + "',TCKimlik3='" + TCKimlikBox3.Text + "',TCKimlik4='" + TCKimlikBox4.Text + "',Telefon='" + TelefonBox.Text + "',BaslangicT='" + baslangicT + "',BitisT='" + bitisT + "' WHERE Oda='" + odacik + "'", baglanti);
            odagunc.ExecuteNonQuery();
            baglanti.Close();
        }

        private string doluluk;
        private void ODA21_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_1";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA21'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA21.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_1";
            }
        }

        private void ODA22_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_2";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA22'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA22.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_2";
            }

        }

        private void ODA23_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_3";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA23'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA23.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_3";
            }

        }

        private void ODA24_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_4";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA24'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA24.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_4";
            }

        }

        private void ODA25_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_5";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA25'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA25.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_5";
            }

        }

        private void ODA26_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_6";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA26'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA26.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_6";
            }

        }

        private void ODA27_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_7";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA27'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA27.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_7";
            }

        }

        private void ODA28_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_8";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA28'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA28.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_8";
            }

        }

        private void ODA29_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 2_9";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA29'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA29.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 2_9";
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnOnayla.Text == "Güncelle")
            {
                btnOnayla.Text = "Onayla";
                btnReddet.Visible = false;
                btnBosalt.Visible = false;
                ClearAll();
            }
            Secilen.Text = "Seçilmedi";
            Secilen.ForeColor = Color.Red;
            OdaBox.Text = "";
            if (comboBox2.SelectedIndex == 0)
            {
                Kat1.Visible = true;
                Kat2.Visible = false;
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                Kat1.Visible = false;
                Kat2.Visible = true;
            }
        }

        private void btnBosalt_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand rezsil = new OleDbCommand("UPDATE Rezervasyon SET AdSoyad='',AdSoyad2='',AdSoyad3='',AdSoyad4='',Onay='BOŞ',Cinsiyet='',Cinsiyet2='',Cinsiyet3='',Cinsiyet4='',Yas='',Yas2='',Yas3='',Yas4='',TCKimlik='',TCKimlik2='',TCKimlik3='',TCKimlik4='',Telefon='',BaslangicT='" + baslangicT + "',BitisT='" + bitisT + "' WHERE Oda='" + odacik + "'", baglanti);
            OleDbCommand tlpsil = new OleDbCommand("DELETE * FROM Talep WHERE Oda='" + odacik + "'", baglanti);
            rezsil.ExecuteNonQuery();
            tlpsil.ExecuteNonQuery();
            baglanti.Close();
            DoluBosKontrol();
            MessageBox.Show("Başarılı.");
            ClearAll();
            OdaBox.Text = "";
            btnOnayla.Text = "Onayla";
            Secilen.Text = "Seçilmedi";
            Secilen.ForeColor = Color.Red;

            btnBosalt.Visible = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == false)
            {
                panel5.Visible = true;
            }
            else
            {
                panel5.Visible = false;
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

        private void BirMusteriIleri_Click(object sender, EventArgs e)
        {
            Musteri2Panel.Visible = true;
        }

        private void IkiMusteriGeri_Click(object sender, EventArgs e)
        {
            Musteri2Panel.Visible = false;
        }

        private void IkiMusteriIleri_Click(object sender, EventArgs e)
        {
            Musteri3Panel.Visible = true;
            Musteri2Panel.Visible = false;
        }

        private void UcMusteriGeri_Click(object sender, EventArgs e)
        {
            Musteri2Panel.Visible = true;
            Musteri3Panel.Visible = false;
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

        private void DortMusteriGeri_Click(object sender, EventArgs e)
        {
            Musteri3Panel.Visible = true;
            Musteri4Panel.Visible = false;
        }

        private void UcMusteriIleri_Click(object sender, EventArgs e)
        {
            Musteri4Panel.Visible = true;
            Musteri3Panel.Visible = false;
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


        private void btnKayitSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bu işlem giriş yapmış olduğunuz hesabı silecek.\nEmin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand personelsil = new OleDbCommand("DELETE * FROM PGiris WHERE AdSoyad='" + PersonelAdi.Text + "'", baglanti);
                personelsil.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Hesap başarıyla silindi.");
                Baslangic baslangic = new Baslangic();
                this.Hide();
                baslangic.Show();
            }
            else
            {

            }
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {
            int Hafizadaki=0;
            Reddet reddet = new Reddet();
            reddet.tc = TCKimlikBox.Text;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            OleDbDataReader dr;
            baglanti.Open();
            OleDbCommand hafizadatut = new OleDbCommand("SELECT Kimlik FROM Talep WHERE TCKimlik='"+TCKimlikBox.Text+"'", baglanti);
            dr = hafizadatut.ExecuteReader();
            if (dr.Read())
            {
                Hafizadaki = Convert.ToInt32(dr[0]);
            }
            baglanti.Close();
            reddet.ShowDialog();
            baglanti.Open();
            string karsilastir = "0";
            OleDbCommand kimliklekarsilastir = new OleDbCommand("SELECT Onay FROM Talep WHERE Kimlik=" + Hafizadaki + "", baglanti);
            dr = kimliklekarsilastir.ExecuteReader();
            if (dr.Read())
            {
                karsilastir = dr[0].ToString();
            }
            baglanti.Close();

            if (karsilastir == "REZERVE")
            {

            }
            else
            {
                baglanti.Open();
                OleDbCommand talepreddet = new OleDbCommand("UPDATE Talep SET Onay='REDDET' WHERE TCKimlik='" + TCKimlikBox.Text + "'", baglanti);
                talepreddet.ExecuteNonQuery();
                baglanti.Close();
                TelefonBox.Enabled = true;
                AdSoyadBox.Enabled = true;
                AdSoyadBox2.Enabled = true;
                AdSoyadBox3.Enabled = true;
                AdSoyadBox4.Enabled = true;
                cmbCinsiyet.Enabled = true;
                cmbCinsiyet2.Enabled = true;
                cmbCinsiyet3.Enabled = true;
                cmbCinsiyet4.Enabled = true;
                YasBox.Enabled = true;
                YasBox2.Enabled = true;
                YasBox3.Enabled = true;
                YasBox4.Enabled = true;
                TCKimlikBox.Enabled = true;
                TCKimlikBox2.Enabled = true;
                TCKimlikBox3.Enabled = true;
                TCKimlikBox4.Enabled = true;
                dtBaslangic.Visible = true;
                dtBitis.Visible = true;
                BaslangicTalep.Visible = false;
                BitisTalep.Visible = false;
                Talepmi = 0;
                ClearAll();
                talepsayisibul();
                OdaBox.Text = "";
                Secilen.Text = "Seçilmedi";
                Secilen.ForeColor = Color.Red;
                btnBosalt.Visible = false;
                btnReddet.Visible = false;
                btnTalepGetir.Enabled = true;
                OdalarEnabled();
            }
            

        }

        int Talepmi = 0;
        private void btnTalepGetir_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ToplamTalepSayisi.Text) > 0)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand talepgetir = new OleDbCommand("SELECT AdSoyad,Cinsiyet,Yas,TCKimlik,Telefon,BaslangicT,BitisT FROM Talep WHERE Onay='REZERVE'",baglanti);
                OleDbCommand talepgetir2 = new OleDbCommand("SELECT AdSoyad2,Cinsiyet2,Yas2,TCKimlik2 FROM Talep WHERE Onay='REZERVE'",baglanti);
                OleDbCommand talepgetir3 = new OleDbCommand("SELECT AdSoyad3,Cinsiyet3,Yas3,TCKimlik3 FROM Talep WHERE Onay='REZERVE'",baglanti);
                OleDbCommand talepgetir4 = new OleDbCommand("SELECT AdSoyad4,Cinsiyet4,Yas4,TCKimlik4 FROM Talep WHERE Onay='REZERVE'",baglanti);
                OleDbDataReader dr = talepgetir.ExecuteReader();
                if (dr.Read())
                {
                    AdSoyadBox.Text = dr[0].ToString();
                    cmbCinsiyet.SelectedItem = dr[1].ToString();
                    YasBox.Text = dr[2].ToString();
                    TCKimlikBox.Text = dr[3].ToString();
                    TelefonBox.Text = dr[4].ToString();
                    dtBaslangic.Value = Convert.ToDateTime(dr[5]);
                    dtBitis.Value = Convert.ToDateTime(dr[6]);
                }
                dr = talepgetir2.ExecuteReader();
                if (dr.Read())
                {
                    AdSoyadBox2.Text = dr[0].ToString();
                    if (Convert.ToString(dr[1]) == "Bay" || Convert.ToString(dr[1]) == "Bayan")
                    {
                        cmbCinsiyet2.SelectedItem = dr[1].ToString();
                    }
                    else
                    {
                        cmbCinsiyet2.SelectedIndex = -1;
                    }
                    YasBox2.Text = dr[2].ToString();
                    TCKimlikBox2.Text = dr[3].ToString();
                }
                dr = talepgetir3.ExecuteReader();
                if (dr.Read())
                {
                    AdSoyadBox3.Text = dr[0].ToString();
                    if (Convert.ToString(dr[1]) == "Bay" || Convert.ToString(dr[1]) == "Bayan")
                    {
                        cmbCinsiyet3.SelectedItem = dr[1].ToString();
                    }
                    else
                    {
                        cmbCinsiyet3.SelectedIndex = -1;
                    }
                    YasBox3.Text = dr[2].ToString();
                    TCKimlikBox3.Text = dr[3].ToString();
                }
                dr = talepgetir4.ExecuteReader();
                if (dr.Read())
                {
                    AdSoyadBox4.Text = dr[0].ToString();
                    if (Convert.ToString(dr[1]) == "Bay"|| Convert.ToString(dr[1]) == "Bayan")
                    {
                        cmbCinsiyet4.SelectedItem = dr[1].ToString();
                    }
                    else
                    {
                        cmbCinsiyet4.SelectedIndex = -1;
                    }
                    YasBox4.Text = dr[2].ToString();
                    TCKimlikBox4.Text = dr[3].ToString();
                }

                OdaBox.Text = "";
                Secilen.Text = "Seçilmedi";
                Secilen.ForeColor = Color.Red;
                MessageBox.Show("Rezervasyon talebi Getirildi.");
                TelefonBox.Enabled = false;
                AdSoyadBox.Enabled = false;
                AdSoyadBox2.Enabled = false;
                AdSoyadBox3.Enabled = false;
                AdSoyadBox4.Enabled = false;
                cmbCinsiyet.Enabled = false;
                cmbCinsiyet2.Enabled = false;
                cmbCinsiyet3.Enabled = false;
                cmbCinsiyet4.Enabled = false;
                YasBox.Enabled = false;
                YasBox2.Enabled = false;
                YasBox3.Enabled = false;
                YasBox4.Enabled = false;
                TCKimlikBox.Enabled = false;
                TCKimlikBox2.Enabled = false;
                TCKimlikBox3.Enabled = false;
                TCKimlikBox4.Enabled = false;
                dtBaslangic.Visible = false;
                dtBitis.Visible = false;
                BaslangicTalep.Visible = true;
                BitisTalep.Visible = true;
                BaslangicTalep.Text = dtBaslangic.Value.ToShortDateString();
                dtBaslangic.Value = Convert.ToDateTime(BaslangicTalep.Text);
                BitisTalep.Text = dtBitis.Value.ToShortDateString();
                dtBitis.Value = Convert.ToDateTime(BitisTalep.Text);

                btnReddet.Visible = true;
                btnBosalt.Visible = false;
                btnOnayla.Text = "Onayla";
                Talepmi = 1;
                btnTalepGetir.Enabled = false;

            baglanti.Close();
                HangiOdaDoluysaDisable();
            }
            else
            {
                MessageBox.Show("Başka rezervasyon talebi yok.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bu işlem giriş yaptığınız hesaptan çıkmanıza sebep olacak.\nEmin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                PersonelKayit personelKayit = new PersonelKayit();
                this.Hide();
                personelKayit.Show();
            }
            else
            {

            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (OdaBox.Text == "")
            {
                MessageBox.Show("Önce oda seçiniz.", "Hata");
            }
            else if (AdSoyadBox.Text == "" || cmbCinsiyet.SelectedIndex==-1 || YasBox.Text == "" || TCKimlikBox.Text == "" || TelefonBox.Text == "")
            {
                MessageBox.Show("İlk müşteri bilgileri zorunludur..", "Hata");
            }
            else
            {
                kisi_sayisi = 0;
                if (AdSoyadBox.Text != "")
                {
                    kisi_sayisi++;
                    if (AdSoyadBox2.Text != "")
                    {
                        kisi_sayisi++;
                    }
                    if (AdSoyadBox3.Text != "")
                    {
                        kisi_sayisi++;
                    }
                    if (AdSoyadBox4.Text != "")
                    {
                        kisi_sayisi++;
                    }
                }

                odacik = OdaBox.Text;
                odaguncelle(odacik);
                tutar = 60 + ((50 * kisi_sayisi) * Convert.ToInt32(tarihfarki.TotalDays));
                if (btnOnayla.Text == "Onayla")
                {
                    MessageBox.Show("Başarılı.\nÜcret: " + tutar + " TL");
                }
                DoluBosKontrol();

                if (AdSoyadBox.Enabled == false)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                    baglanti.Open();
                    OleDbCommand taleponayla = new OleDbCommand("UPDATE Talep SET Onay='ONAYLI',Oda='" + OdaBox.Text + "' WHERE AdSoyad='" + AdSoyadBox.Text + "'", baglanti);
                    taleponayla.ExecuteNonQuery();
                    baglanti.Close();
                    TelefonBox.Enabled = true;
                    AdSoyadBox.Enabled = true;
                    AdSoyadBox2.Enabled = true;
                    AdSoyadBox3.Enabled = true;
                    AdSoyadBox4.Enabled = true;
                    cmbCinsiyet.Enabled = true;
                    cmbCinsiyet2.Enabled = true;
                    cmbCinsiyet3.Enabled = true;
                    cmbCinsiyet4.Enabled = true;
                    YasBox.Enabled = true;
                    YasBox2.Enabled = true;
                    YasBox3.Enabled = true;
                    YasBox4.Enabled = true;
                    TCKimlikBox.Enabled = true;
                    TCKimlikBox2.Enabled = true;
                    TCKimlikBox3.Enabled = true;
                    TCKimlikBox4.Enabled = true;
                    dtBaslangic.Visible = true;
                    dtBitis.Visible = true;
                    BaslangicTalep.Visible = false;
                    BitisTalep.Visible = false;
                    btnTalepGetir.Enabled = true;
                }
                OdaBox.Text = "";
                Secilen.Text = "Seçilmedi";
                Secilen.ForeColor = Color.Red;
                btnBosalt.Visible = false;
                btnReddet.Visible = false;
                Talepmi = 0;
                ClearAll();
                talepsayisibul();
                OdalarEnabled();
            }
        }
        string on;
        private void PersonelAnaSayfa_Load(object sender, EventArgs e)
        {

            dtbugun = DateTime.Now;
            bugun = dtbugun.ToShortDateString();
            dtBaslangic.Value = Convert.ToDateTime(bugun);
            dtBitis.Value = Convert.ToDateTime(bugun).AddDays(1);
            baslangicT = dtBaslangic.Value.ToShortDateString();
            bitisT = dtBitis.Value.ToShortDateString();
            dtBitis.Value = dtBaslangic.Value.AddDays(1);
            lblbugun.Text = bugun;
            lblbaslangic.Text = baslangicT;
            lblBitis.Text = bitisT;
            comboBox2.SelectedIndex = 0;

            talepsayisibul();
            OdaDuzelt();
            DoluBosKontrol();
            PersonelSayisiBul();
        }

        private void ODA11_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_1";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA11'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA11.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_1";
            }

        }

        private void ODA12_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_2";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA12'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA12.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_2";
            }

        }

        private void ODA13_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_3";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA13'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA13.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_3";
            }

        }

        private void ODA14_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_4";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA14'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA14.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_4";
            }

        }

        private void ODA15_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_5";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA15'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA15.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_5";
            }

        }

        private void ODA16_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_6";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA16'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA16.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_6";
            }

        }

        private void ODA17_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_7";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA17'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA17.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_7";
            }

        }

        private void ODA18_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_8";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA18'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA18.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_8";
            }

        }

        private void ODA19_Click(object sender, EventArgs e)
        {
            if (Talepmi == 0)
            {
                OdaBox.Text = "ODA 1_9";
                odacik = OdaBox.Text;
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand bossakalsin = new OleDbCommand("SELECT Onay FROM rezervasyon WHERE OdaButon='ODA19'", baglanti);
                OleDbDataReader dr = bossakalsin.ExecuteReader();
                if (dr.Read())
                {
                    doluluk = dr[0].ToString();
                    if (doluluk == "DOLU")
                    {
                        OdaGetir(odacik);
                    }
                    else if (doluluk == "BOŞ")
                    {
                        if (btnOnayla.Text == "Güncelle")
                        {
                            OdaGetir(odacik);
                        }
                    }
                }
                baglanti.Close();
                if (ODA19.BackColor == Color.FromArgb(255, 240, 187, 204))
                {
                    btnBosalt.Visible = true;
                }
                else
                {
                    btnBosalt.Visible = false;
                }
                Secilen.Text = odacik;
                Secilen.ForeColor = SystemColors.ControlText;
                panelclear();
            }
            else
            {
                OdaBox.Text = "ODA 1_9";
            }

        }



        private void dtBitis_onValueChanged(object sender, EventArgs e)
        {
            if (dtBitis.Value < dtBaslangic.Value.AddDays(1))
            {
                MessageBox.Show("Rezervasyon bitiş tarihi başlangıç tarihine eşit veya küçük olamaz.", "Uyarı");
                dtBitis.Value = dtBaslangic.Value.AddDays(1);
            }
            bitisT = dtBitis.Value.ToShortDateString();
            tarihfarki = Convert.ToDateTime(bitisT) - Convert.ToDateTime(baslangicT);
            lblSonuc.Text = tarihfarki.TotalDays.ToString();

            lblBitis.Text = bitisT;
            tarihfarki = Convert.ToDateTime(bitisT) - Convert.ToDateTime(baslangicT);
            lblSonuc.Text = tarihfarki.TotalDays.ToString();
        }

        string boxiptal;

        private void dtBaslangic_onValueChanged(object sender, EventArgs e)
        {
            if (dtBaslangic.Value < Convert.ToDateTime(bugun))
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
                baglanti.Open();
                OleDbCommand messageboxiptal = new OleDbCommand("SELECT Onay FROM Rezervasyon WHERE Oda='" + odacik + "'", baglanti);
                OleDbDataReader dr = messageboxiptal.ExecuteReader();
                if (dr.Read())
                {
                    boxiptal = dr[0].ToString();
                }
                if (boxiptal != "DOLU")
                {
                    MessageBox.Show("Geçmişe rezervasyon yapamazsınız.", "Uyarı");
                    dtBaslangic.Value = Convert.ToDateTime(bugun);
                    baslangicT = dtBaslangic.Value.ToShortDateString();
                }
                baglanti.Close();
            }
            if (dtBaslangic.Value > dtBitis.Value)
            {
                dtBitis.Value = dtBaslangic.Value.AddDays(1);
            }
            baslangicT = dtBaslangic.Value.ToShortDateString();

            lblbaslangic.Text = baslangicT;
            tarihfarki = Convert.ToDateTime(bitisT) - Convert.ToDateTime(baslangicT);
            lblSonuc.Text = tarihfarki.TotalDays.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GeriTusu_Click(object sender, EventArgs e)
        {
            Baslangic baslangic = new Baslangic();
            this.Hide();
            baslangic.Show();
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

    }
}
