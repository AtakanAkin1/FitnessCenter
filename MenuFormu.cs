using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitnessCenter
{
    public partial class MenuFormu : MetroFramework.Forms.MetroForm
    {
        public MenuFormu()
        {
            InitializeComponent();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            UyeEklemeFormu uyeekle = new UyeEklemeFormu();
            uyeekle.Show();
            this.Hide();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            UyeSilmeveGuncellemeFormu guncelle = new UyeSilmeveGuncellemeFormu();
            guncelle.Show();
            this.Hide();
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            UyeListemeFormu listele = new UyeListemeFormu();
            listele.Show();
            this.Hide();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            Odeme frm = new Odeme();
            frm.Show();
            this.Hide();
        }
    }
}
