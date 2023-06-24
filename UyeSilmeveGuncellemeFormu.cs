using DocumentFormat.OpenXml.Office2010.Excel;
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
    public partial class UyeSilmeveGuncellemeFormu : MetroFramework.Forms.MetroForm
    {
        public UyeSilmeveGuncellemeFormu()
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

        private void metroButton3_Click(object sender, EventArgs e)
        {
            AdTb.Text = "";
            SoyadTb.Text = "";
            TelefonTb.Text = "";
            CinsiyetCb.Text = "";
            YasTb.Text = "";
            OdemeTb.Text = "";
            ZamanlamaCb.Text = "";
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            MenuFormu anasayfa = new MenuFormu();
            anasayfa.Show();
            this.Hide();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Silinecek Uyeyi Seciniz");
            }
            else
            {
                try
                {
                    Database.Open();
                    string query = "DELETE FROM Uyetbl WHERE Uye_id=" + id + ";";
                    SqlCommand komut = new SqlCommand(query, Database);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Uye Basariyla silindi");
                    Database.Close();
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (id == 0 || AdTb.Text == "" || SoyadTb.Text == "" || TelefonTb.Text == "" || CinsiyetCb.Text == "" || YasTb.Text == "" || OdemeTb.Text == "" || ZamanlamaCb.Text == "")
            {
                MessageBox.Show("Eksik bilgi girisi");
            }
            else
            {
                try
                {
                    Database.Open();
                    string query = "UPDATE Uyetbl SET Uye_adi='" + AdTb.Text + "',Uye_Soyadi='" + SoyadTb.Text + "',Uye_Telefon='" + TelefonTb.Text + "',Uye_Cinsiyet='" + CinsiyetCb.Text + "',Uye_Yas='" + YasTb.Text + "',Uye_Odeme='" + OdemeTb.Text + "',Zamanlama='" + ZamanlamaCb.Text + "' WHERE Uye_id=" + id + ";";
                    SqlCommand komut = new SqlCommand(query, Database);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Uye Basariyla Guncellendi");
                    Database.Close();
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void UyeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(UyeDGV.SelectedRows[0].Cells[0].Value.ToString());
            this.AdTb.Text = UyeDGV.SelectedRows[0].Cells[1].Value.ToString();
            this.SoyadTb.Text = UyeDGV.SelectedRows[0].Cells[2].Value.ToString();
            this.TelefonTb.Text = UyeDGV.SelectedRows[0].Cells[3].Value.ToString();
            this.CinsiyetCb.Text = UyeDGV.SelectedRows[0].Cells[4].Value.ToString();
            this.YasTb.Text = UyeDGV.SelectedRows[0].Cells[5].Value.ToString();
            this.OdemeTb.Text = UyeDGV.SelectedRows[0].Cells[6].Value.ToString();
            this.ZamanlamaCb.Text = UyeDGV.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void UyeSilmeveGuncellemeFormu_Load(object sender, EventArgs e)
        {
            uyeler();
        }
        int id = 0;
    }
}
