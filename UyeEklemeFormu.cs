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
    public partial class UyeEklemeFormu : MetroFramework.Forms.MetroForm
    {
        public UyeEklemeFormu()
        {
            InitializeComponent();
        }
        SqlConnection Database = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ah1ol\OneDrive\Masaüstü\FitnessCenter\SporSalonu.mdf;Integrated Security=True");
        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (AdTB.Text == "" && SoyadTB.Text == "" && TelefonTB.Text == "" && OdemeTB.Text == "" && YasTB.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    Database.Open();
                    string query = "INSERT INTO Uyetbl VALUES('" + AdTB.Text + "','" + SoyadTB.Text + "','" + TelefonTB.Text + "','" + CinsiyetCB.SelectedItem.ToString() + "','" + YasTB.Text + "','" + OdemeTB.Text + "','" + ZamanlamaCB.SelectedItem.ToString() + "')";
                    SqlCommand komut = new SqlCommand(query, Database);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Üye Başarıyla Eklendi");
                    Database.Close();
                    AdTB.Text = "";
                    SoyadTB.Text = "";
                    TelefonTB.Text = "";
                    CinsiyetCB.Text = "";
                    OdemeTB.Text = "";
                    YasTB.Text = "";
                    ZamanlamaCB.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            AdTB.Text = "";
            SoyadTB.Text = "";
            TelefonTB.Text = "";
            CinsiyetCB.Text = "";
            OdemeTB.Text = "";
            YasTB.Text = "";
            ZamanlamaCB.Text = "";
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            MenuFormu anasayfa = new MenuFormu();
            anasayfa.Show();
            this.Hide();
        }
    }
}
