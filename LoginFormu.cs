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
    public partial class LoginFormu : MetroFramework.Forms.MetroForm
    {
        public LoginFormu()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = KullaniciaT.Text;
            string sifre = SifreTb.Text;

            if (kullaniciAdi == "" || sifre == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                SqlConnection Database = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ah1ol\OneDrive\Masaüstü\FitnessCenter\SporSalonu.mdf;Integrated Security=True");
                Database.Open();

                string sorgu = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @kullaniciAdi AND Sifre= @sifre";
                SqlCommand komut = new SqlCommand(sorgu, Database);
                komut.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                komut.Parameters.AddWithValue("@sifre", sifre);

                int kullaniciSayisi = (int)komut.ExecuteScalar();

                Database.Close();

                if (kullaniciSayisi > 0)
                {
                    KullaniciOdemeFormu kOdeme = new KullaniciOdemeFormu();
                    kOdeme.Show();
                    this.Hide();
                }
                else if (KullaniciaT.Text.ToLower() == "admin" && SifreTb.Text.ToLower() == "1234")
                {
                    MenuFormu anasayfa = new MenuFormu();
                    anasayfa.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı");
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            KullaniciaT.Text = "";
            SifreTb.Text = "";
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            KayitOL kayıtOlma = new KayitOL();
            kayıtOlma.Show();
            this.Hide();
        }
    }
}
