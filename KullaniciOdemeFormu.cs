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
    public partial class KullaniciOdemeFormu : MetroFramework.Forms.MetroForm
    {
        public KullaniciOdemeFormu()
        {
            InitializeComponent();
        }
        SqlConnection Database = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ah1ol\OneDrive\Masaüstü\FitnessCenter\SporSalonu.mdf;Integrated Security=True");
        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (AdTb.Text == "" || SoyadTb.Text == "" || OdemeTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                string odemetarih = tarih.Value.Month.ToString() + tarih.Value.Year.ToString();
                Database.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT (Odeme_Miktar) FROM Odeme WHERE Odeme_UyeA='" + AdTb.Text.ToString() + "' AND Odeme_UyeS='" + SoyadTb.Text.ToString() + "' AND Odeme_Ay='" + odemetarih + "'", Database);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    MessageBox.Show("Bu Ayki Ödeme Zaten Yapılmıştır");
                }
                else
                {
                    string query = "INSERT INTO Odeme VALUES('" + odemetarih + "','" + AdTb.Text.ToString() + "','" + SoyadTb.Text.ToString() + "','" + OdemeTb.Text + "')";
                    SqlCommand komut = new SqlCommand(query, Database);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ödeme Başarılı");
                }
                Database.Close();
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
