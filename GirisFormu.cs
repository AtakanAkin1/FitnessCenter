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
    public partial class GirisFormu : MetroFramework.Forms.MetroForm
    {
        public GirisFormu()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            metroPanel2.Width += 20;
            if (metroPanel2.Width >= 735)
            {
                timer1.Stop();
                LoginFormu login = new LoginFormu();
                login.Show();
                this.Hide();
            }
        }

        
    }
}
