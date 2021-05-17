using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odevdeneme2
{
    public partial class AraPanel : Form
    {
        public string tc { get; set; }
        public AraPanel()
        {
            InitializeComponent();
        }
        // BU PANEL SADECE ARA SAHNELERDEN GEÇİŞ EKRANI
        private void button1_Click(object sender, EventArgs e)
        {
            
            AlısverisEkranı f = new AlısverisEkranı();
            f.giristc = tc;
            f.Show();
            this.Hide();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlıcıVeSatıcıBilgileri alıcıVeSatıcı= new AlıcıVeSatıcıBilgileri();
            alıcıVeSatıcı.tc = tc;
            alıcıVeSatıcı.Show();
            this.Hide();
        }

        private void AraPanel_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
