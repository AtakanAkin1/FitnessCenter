using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessCenter
{
    public partial class KayitOL : MetroFramework.Forms.MetroForm
    {
        public KayitOL()
        {
            InitializeComponent();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = KullaniciTxt.Text;
            string sifre = SifreTxt.Text;

            SqlConnection Database = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ah1ol\OneDrive\Masaüstü\FitnessCenter\SporSalonu.mdf;Integrated Security=True");
            Database.Open();

            string kayitSorgusu = "INSERT INTO Kullanicilar (KullaniciAdi, Sifre) VALUES (@kullaniciadi, @sifre)";
            SqlCommand komut = new SqlCommand(kayitSorgusu, Database);
            komut.Parameters.AddWithValue("@kullaniciadi", kullaniciAdi);
            komut.Parameters.AddWithValue("@sifre", sifre);
            komut.ExecuteNonQuery();

            Database.Close();
            MessageBox.Show("Sisteme kayıt oldunuz, kullanıcı adınızı ve şifrenizi kullanarak sisteme giriş yapabilirsiniz.");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            LoginFormu login = new LoginFormu();
            login.Show();
            this.Hide();
        }
    }
}
