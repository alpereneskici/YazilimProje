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
    public partial class AlıcıVeSatıcıBilgileri : Form
    {
        public AlıcıVeSatıcıBilgileri()
        {
            InitializeComponent();
        }


        public string tc { get; set; }
        public string usertypes { get; set; }
        private void AlıcıVeSatıcıBilgileri_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
            textBoxUrunMk.Visible = false;
            label2.Visible = false;
            textBoxPm.Visible = false;
         
          
            label4.Visible = false;
            textBox1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // seçime göre textbox geitriyor ekrana
            if(comboBox1.SelectedItem.ToString()== "Alıcı")
            {
                label2.Visible = true;
                textBoxPm.Visible = true;
                label3.Visible = false;
                textBoxUrunMk.Visible = false;
                label4.Visible = false;
                textBox1.Visible = false;
            }
            else if(comboBox1.SelectedItem.ToString() == "Satıcı")
            {
                textBoxPm.Text = "";
                label4.Visible = true;
                label4.Text = "Ürün Birim Fiyat'ı (KG/TL ";
                textBox1.Visible = true;
                label2.Visible = true;
                textBoxPm.Visible = true;
                label3.Visible = true;
                textBoxUrunMk.Visible = true;
                label3.Text = "Ürün Miktarı";
                label2.Text = "Ürün Türü";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerManager accsessmanager = new CustomerManager(new AccesCustomerDAL());
          
            List<string> urunler = new List<string>();
            List<string> urunid = new List<string>();
            urunler = accsessmanager.select(tc,"TC","UrunTipi","Urunler"); // giriş yapan kullanıcının sahip olduğu ürünleri alıyor
            urunid = accsessmanager.select(tc, "TC", "id", "Urunler");
         
            if (comboBox1.SelectedItem.ToString() == "Alıcı")
            {
                
                accsessmanager.Update("Banka", "OnayBekleyenBakiye", textBoxPm.Text, tc, "TC");
                accsessmanager.Update("Banka", "BakiyeOnayDurumu", "Onay Bekleniyor", tc, "TC");
            }
            else if (comboBox1.SelectedItem.ToString() == "Satıcı")
            {
             
                if (urunler.Contains(textBoxPm.Text)) // eğer kullanıcı bu ürünü daha önce listelediyse sadece stok güncellemesi yapıyor     
                {
                    int a = urunler.IndexOf(textBoxPm.Text);
                    string b=urunid[a];
                    int c = Convert.ToInt32(b);
                
                    accsessmanager.Update("Urunler", "OnayBekleyenUrunMiktar", textBoxUrunMk.Text,c, "id");
                    accsessmanager.Update("Urunler", "UrunOnayDurumu", "Onay Bekleniyor", c, "id");
                    accsessmanager.Update("Urunler", "UrunSatisFiyatı", textBox1.Text, c, "id");
                }
                else  // eğer kullanıcı bu ürünü daha önce listelemediyse ekleme işlemi yapıyor 
                {
                   
                    accsessmanager.CustomerAdd(tc, textBoxPm.Text, "TC", "UrunTipi", "Urunler");
                    urunler = accsessmanager.select(tc, "TC", "UrunTipi", "Urunler");
                    urunid = accsessmanager.select(tc, "TC", "id", "Urunler");
                    int a = urunler.IndexOf(textBoxPm.Text);
                    string b = urunid[a];
                    int c = Convert.ToInt32(b);
                    accsessmanager.Update("Urunler", "UrunMiktarı", "0", c, "id");
                    accsessmanager.Update("Urunler", "OnayBekleyenUrunMiktar", textBoxUrunMk.Text, c, "id");
                    accsessmanager.Update("Urunler", "UrunOnayDurumu", "Onay Bekleniyor", c, "id");
                    accsessmanager.Update("Urunler", "UrunSatisFiyatı", textBox1.Text, c, "id");
                }
               

            }
             
              
            }

        private void button2_Click(object sender, EventArgs e)
        {
            AraPanel f1 = new AraPanel();
            f1.Show();
            this.Hide();
        }
    }
    }

