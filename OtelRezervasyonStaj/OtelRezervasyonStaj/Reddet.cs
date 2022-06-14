using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OtelRezervasyonStaj
{
    public partial class Reddet : Form
    {
        public Reddet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string tc;
        private void btnOnayla_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\RezervOtelDatabase.accdb");
            baglanti.Open();
            OleDbCommand sebepdegistir = new OleDbCommand("UPDATE Talep SET Onay='REDDET',Sebep='"+richTextBox1.Text+"' WHERE TCKimlik='" + tc + "'", baglanti);
            sebepdegistir.ExecuteNonQuery();
            MessageBox.Show("Başarılı.");
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                btnOnayla.Enabled = false;
            }
            else
            {
                btnOnayla.Enabled = true;
            }
        }

        private void Reddet_Load(object sender, EventArgs e)
        {

        }
    }
}
