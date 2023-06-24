using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace FitnessCenter
{
    public partial class Odeme : MetroFramework.Forms.MetroForm
    {
        public Odeme()
        {
            InitializeComponent();
        }
        SqlConnection Database = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ah1ol\OneDrive\Masaüstü\FitnessCenter\SporSalonu.mdf;Integrated Security=True");
        private void UyeleriAra()
        {
            Database.Open();
            string query = "SELECT * FROM Odeme WHERE Odeme_UyeA=@UyeAdi AND Odeme_UyeS=@UyeSoyadi";
            SqlDataAdapter sda = new SqlDataAdapter(query, Database);
            sda.SelectCommand.Parameters.AddWithValue("@UyeAdi", AdTb.Text);
            sda.SelectCommand.Parameters.AddWithValue("@UyeSoyadi", SoyadTb.Text);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            OdemeDGV.DataSource = ds.Tables[0];
            Database.Close();

        }
        private void uyeler()
        {
            Database.Open();
            string query = "SELECT * FROM Odeme";
            SqlDataAdapter sda = new SqlDataAdapter(query, Database);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            OdemeDGV.DataSource = ds.Tables[0];
            Database.Close();
        }


        private void AraTb_Click(object sender, EventArgs e)
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

        private void metroButton2_Click(object sender, EventArgs e)
        {
            uyeler();
            AdTb.Text = "";
            SoyadTb.Text = "";
            OdemeTb.Text = "";
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            MenuFormu anasayfa = new MenuFormu();
            anasayfa.Show();
            this.Hide();
        }

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
                uyeler();
            }
        }

        private void ExceleAktar_Click(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "Workbook.xlsx");

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
                sheets.Append(sheet);

                Row headerRow = new Row();
                for (int i = 0; i < OdemeDGV.Columns.Count; i++)
                {
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(OdemeDGV.Columns[i].HeaderText);
                    headerRow.AppendChild(cell);
                }

                worksheetPart.Worksheet.GetFirstChild<SheetData>().AppendChild(headerRow);

                for (int i = 0; i < OdemeDGV.Rows.Count; i++)
                {
                    Row dataRow = new Row();
                    for (int j = 0; j < OdemeDGV.Columns.Count; j++)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(OdemeDGV[j, i].Value.ToString());
                        dataRow.AppendChild(cell);
                    }
                    worksheetPart.Worksheet.GetFirstChild<SheetData>().AppendChild(dataRow);
                }
                workbookPart.Workbook.Save();
                MessageBox.Show("Excel dosyanız masaüstüne oluşturuldu.");
            }
        }

        private void Odeme_Load(object sender, EventArgs e)
        {
            uyeler();
        }
    }
}
