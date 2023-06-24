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
    public partial class UyeListemeFormu : MetroFramework.Forms.MetroForm
    {
        public UyeListemeFormu()
        {
            InitializeComponent();
        }
        SqlConnection Database = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ah1ol\OneDrive\Masaüstü\FitnessCenter\SporSalonu.mdf;Integrated Security=True");
        private void uyeler()
        {
            Database.Open();
            string query = "SELECT * FROM Uyetbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Database);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            UyeDGV.DataSource = ds.Tables[0];
            Database.Close();
        }
        private void UyeleriAra()
        {
            Database.Open();
            string query = "SELECT * FROM Uyetbl WHERE Uye_Adi=@UyeAdi AND Uye_Soyadi=@UyeSoyadi";
            SqlDataAdapter sda = new SqlDataAdapter(query, Database);
            sda.SelectCommand.Parameters.AddWithValue("@UyeAdi", AdTb.Text);
            sda.SelectCommand.Parameters.AddWithValue("@UyeSoyadi", SoyadTb.Text);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UyeDGV.DataSource = ds.Tables[0];
            Database.Close();

        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            uyeler();
            AdTb.Text = "";
            SoyadTb.Text = "";
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            MenuFormu anasayfa = new MenuFormu();
            anasayfa.Show();
            this.Hide();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (AdTb.Text == "" && SoyadTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi!");
            }
            else
            {
                UyeleriAra();
            }
        }

        private void UyeListemeFormu_Load(object sender, EventArgs e)
        {
            uyeler();
        }
    }
}
